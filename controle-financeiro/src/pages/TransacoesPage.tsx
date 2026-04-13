import React, { useState } from 'react';
import Swal from 'sweetalert2';
import styles from '../styles/ListPageLayout.module.css';
import { usePessoas } from '../hooks/usePessoas';
import { useCategorias } from '../hooks/useCategorias';
import { useTransacoes } from '../hooks/useTransacoes';
import { useModal } from '../hooks/useModal';
import { FinalidadeCategoria, TipoTransacaoValor, type TipoTransacao } from '../types/categoria.types';
import { DataTable } from '../components/common/DataTable/DataTable';
import { Modal } from '../components/common/Modal/Modal';
import { TransacaoForm } from '../components/features/Transacoes/TransacaoForm';

export const TransacoesPage: React.FC = () => {
    const { transacoes, loading, error, createTransacao } = useTransacoes();
    const { pessoas } = usePessoas();
    const { categorias } = useCategorias();
    const { isOpen, openModal, closeModal } = useModal();
    const [filterTipo, setFilterTipo] = useState<TipoTransacao | 'todas'>('todas');
    const [filterPessoa, setFilterPessoa] = useState<number | 'todas'>('todas');
    const [filterCategoria, setFilterCategoria] = useState<number | 'todas'>('todas');
    const [filterBusca, setFilterBusca] = useState('');

    const filteredTransacoes = transacoes.filter(transacao => {
        if (filterTipo !== 'todas' && transacao.tipo !== filterTipo) return false;
        if (filterPessoa !== 'todas' && transacao.idPessoa !== filterPessoa) return false;

        if (filterCategoria !== 'todas') {
            const selectedCategoria = categorias.find(c => c.id === filterCategoria);
            if (selectedCategoria) {
                if (selectedCategoria.finalidade === FinalidadeCategoria.Ambas) {
                    const transacaoCategoria = categorias.find(c => c.id === transacao.idCategoria);
                    if (!transacaoCategoria) return false;
                    if (
                        transacaoCategoria.finalidade !== FinalidadeCategoria.Despesa &&
                        transacaoCategoria.finalidade !== FinalidadeCategoria.Receita
                    ) {
                        return false;
                    }
                } else if (transacao.idCategoria !== filterCategoria) {
                    return false;
                }
            } else {
                return false;
            }
        }

        const q = filterBusca.trim().toLowerCase();
        if (q && !transacao.descricao.toLowerCase().includes(q)) return false;
        return true;
    });

    const enrichedTransacoes = filteredTransacoes.map(transacao => ({

        ...transacao,
        pessoaNome: pessoas.find(p => p.id === transacao.idPessoa)?.nome || 'N/A',
        idPessoaade: pessoas.find(p => p.id === transacao.idPessoa)?.idade || 0,
        categoriaNome: categorias.find(c => c.id === transacao.idCategoria)?.descricao || 'N/A',
        categoriaFinalidade: categorias.find(c => c.id === transacao.idCategoria)?.finalidade || 'N/A',
    }));



    const formatCurrency = (value: number) => {
        return new Intl.NumberFormat('pt-BR', {
            style: 'currency',
            currency: 'BRL'
        }).format(value);
    };

    const columns = [
        { key: 'descricao' as const, header: 'Descrição' },
        {
            key: 'valor' as const,
            header: 'Valor',
            render: (value: number) => formatCurrency(value)
        },
        {
            key: 'tipo' as const,
            header: 'Tipo',
            render: (value: TipoTransacao) => (
                <span
                    className={
                        value === TipoTransacaoValor.Receita ? styles.tipoReceita : styles.tipoDespesa
                    }
                >
                    {value === TipoTransacaoValor.Receita ? '📈 Receita' : '📉 Despesa'}
                </span>
            )
        },
        {
            key: 'categoriaNome' as const,
            header: 'Categoria',
            render: (value: string, item: any) => (
                <div className={styles.categoriaCell}>
                    <span>{value}</span>
                    <span className={styles.categoriaBadge}>
                        {item.categoriaFinalidade === 'ambas' ? 'Ambas' : item.categoriaFinalidade}
                    </span>
                </div>
            )
        },
        {
            key: 'pessoaNome' as const,
            header: 'Pessoa',
            render: (value: string, item: any) => (
                <div className={styles.pessoaCell}>
                    <span>{value}
                    {item.idPessoaade < 18 && (
                        <span className={styles.menorIdadeBadge}>⚠️ Menor</span>
                    )}
                    </span>
                </div>
            )
        },
    ];

    const handleSubmit = async (data: any) => {
        const response = await createTransacao(data);
        if (response?.sucesso) {
            closeModal();
        } else {
            Swal.fire({
                icon: 'info',
                title: 'Informação',
                text: response?.mensagem ?? 'Não foi possível criar a transação',
            });
        }
    };

    const handleAddNew = () => {
        openModal();
    };

    const handleExportCSV = () => {
        const headers = ['Descrição', 'Valor', 'Tipo', 'Categoria', 'Pessoa'];
        const data = enrichedTransacoes.map(t => [
            t.descricao,
            t.valor,
            t.tipo === TipoTransacaoValor.Receita ? 'Receita' : 'Despesa',
            t.categoriaNome,
            t.pessoaNome
        ]);

        const csvContent = [
            headers.join(','),
            ...data.map(row => row.join(','))
        ].join('\n');

        const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });
        const link = document.createElement('a');
        const url = URL.createObjectURL(blob);
        link.href = url;
        link.setAttribute('download', 'transacoes.csv');
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        URL.revokeObjectURL(url);
    };

    const handleClearFilters = () => {
        setFilterTipo('todas');
        setFilterPessoa('todas');
        setFilterCategoria('todas');
        setFilterBusca('');
    };

    if (loading && transacoes.length === 0) {
        return (
            <div className={styles.container}>
                <div className={styles.loadingContainer}>
                    <div className={styles.spinner} />
                    <p>Carregando transações...</p>
                </div>
            </div>
        );
    }

    if (error) {
        return (
            <div className={styles.container}>
                <div className={styles.errorContainer}>
                    <div className={styles.errorIcon}>⚠️</div>
                    <h3>Erro ao carregar transações</h3>
                    <p>{error}</p>
                </div>
            </div>
        );
    }

    return (
        <div className={styles.container}>
            <div className={styles.header}>
                <div>
                    <h1 className={styles.title}>Transações</h1>
                    <p className={styles.subtitle}>Gerencie todas as transações financeiras</p>
                </div>
                <div className={styles.headerActions}>
                    <button onClick={handleExportCSV} className={styles.exportButton}>
                        📥 Exportar CSV
                    </button>
                    <button onClick={handleAddNew} className={styles.addButton}>
                        + Nova Transação
                    </button>
                </div>
            </div>
            <div className={styles.filtersCard}>
                <div className={styles.filtersHeader}>
                    <h3 className={styles.filtersTitle}>🔍 Filtros</h3>
                    <button onClick={handleClearFilters} className={styles.clearFilters}>
                        Limpar Filtros
                    </button>
                </div>
                <div className={styles.filtersGrid}>
                    <div className={styles.filterGroup}>
                        <label className={styles.filterLabel}>Tipo</label>
                        <select
                            value={filterTipo === 'todas' ? 'todas' : String(filterTipo)}
                            onChange={(e) => {
                                const v = e.target.value;
                                setFilterTipo(
                                    v === 'todas' ? 'todas' : (Number(v) as TipoTransacao)
                                );
                            }}
                            className={styles.filterSelect}
                        >
                            <option value="todas">Todas</option>
                            <option value={String(TipoTransacaoValor.Despesa)}>Despesas</option>
                            <option value={String(TipoTransacaoValor.Receita)}>Receitas</option>
                        </select>
                    </div>

                    <div className={styles.filterGroup}>
                        <label className={styles.filterLabel}>Pessoa</label>
                        <select
                            value={filterPessoa === 'todas' ? 'todas' : String(filterPessoa)}
                            onChange={(e) =>
                                setFilterPessoa(
                                    e.target.value === 'todas' ? 'todas' : Number(e.target.value)
                                )
                            }
                            className={styles.filterSelect}
                        >
                            <option value="todas">Todas as pessoas</option>
                            {pessoas.map(pessoa => (
                                <option key={pessoa.id} value={pessoa.id}>
                                    {pessoa.nome} ({pessoa.idade} anos)
                                </option>
                            ))}
                        </select>
                    </div>

                    <div className={styles.filterGroup}>
                        <label className={styles.filterLabel}>Categoria</label>
                        <select
                            value={filterCategoria === 'todas' ? 'todas' : String(filterCategoria)}
                            onChange={(e) =>
                                setFilterCategoria(
                                    e.target.value === 'todas' ? 'todas' : Number(e.target.value)
                                )
                            }
                            className={styles.filterSelect}
                        >
                            <option value="todas">Todas as categorias</option>
                            {categorias.map(categoria => {
                                const descricaoNormalizada = categoria.descricao.toLowerCase();
                                const mostrarFinalidade = !['despesa', 'receita', 'ambas'].includes(descricaoNormalizada);

                                return (
                                    <option key={categoria.id} value={categoria.id}>
                                        {categoria.descricao}
                                        {mostrarFinalidade && (
                                            <> ({categoria.finalidade == 1 ? 'Despesa' : categoria.finalidade == 2 ? 'Receita' : 'Ambas'})</>
                                        )}
                                    </option>
                                );
                            })}
                        </select>
                    </div>

                    <div className={styles.filterGroup}>
                        <label className={styles.filterLabel}>Buscar na descrição</label>
                        <input
                            type="search"
                            value={filterBusca}
                            onChange={e => setFilterBusca(e.target.value)}
                            placeholder="Texto da transação..."
                            className={styles.filterInput}
                        />
                    </div>
                </div>
            </div>

            <div className={styles.tableContainer}>
                {enrichedTransacoes.length === 0 ? (
                    <div className={styles.emptyState}>
                        <div className={styles.emptyIcon}>💰</div>
                        <h3>Nenhuma transação encontrada</h3>
                        <p>Não há transações cadastradas ou os filtros não retornaram resultados.</p>
                    </div>
                ) : (
                    <>
                        <div className={styles.tableHeader}>
                            <div className={styles.tableTitle}>
                                <span>📋 Lista de Transações</span>
                                <span className={styles.tableCount}>{enrichedTransacoes.length} registros</span>
                            </div>
                        </div>
                        <DataTable
                            columns={columns}
                            data={enrichedTransacoes}
                            actions={false}
                        />
                    </>
                )}
            </div>

            <Modal isOpen={isOpen} onClose={closeModal} title="Nova Transação" closeOnBackdropClick={false} closeOnEsc={false}>
                <TransacaoForm onSubmit={handleSubmit} onCancel={closeModal} />
            </Modal>
        </div>
    );
};