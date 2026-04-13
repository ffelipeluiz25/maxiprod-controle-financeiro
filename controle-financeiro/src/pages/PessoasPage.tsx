import React, { useMemo, useState } from 'react';
import Swal from 'sweetalert2';
import type { Pessoa, PessoaInput } from '../types/pessoa.types';
import { useModal } from '../hooks/useModal';
import { DataTable } from '../components/common/DataTable/DataTable';
import { Modal } from '../components/common/Modal/Modal';
import { usePessoas } from '../hooks/usePessoas';
import { PessoaForm } from '../components/features/Pessoas/PessoaForm';
import styles from '../styles/ListPageLayout.module.css';

type FaixaIdade = 'todas' | 'menor' | 'adulto';

export const PessoasPage: React.FC = () => {
    const { pessoas, loading, error, createPessoa, updatePessoa, deletePessoa } = usePessoas();
    const [editingPessoa, setEditingPessoa] = useState<Pessoa | null>(null);
    const { isOpen, openModal, closeModal } = useModal();
    const [filterNome, setFilterNome] = useState('');
    const [filterFaixa, setFilterFaixa] = useState<FaixaIdade>('todas');

    const filteredPessoas = useMemo(() => {
        return pessoas.filter(p => {
            const q = filterNome.trim().toLowerCase();
            if (q && !p.nome.toLowerCase().includes(q)) return false;
            if (filterFaixa === 'menor' && p.idade >= 18) return false;
            if (filterFaixa === 'adulto' && p.idade < 18) return false;
            return true;
        });
    }, [pessoas, filterNome, filterFaixa]);

    const columns = [
        { key: 'nome' as const, header: 'Nome' },
        {
            key: 'idade' as const,
            header: 'Idade',
            render: (value: number) => (
                <div className={styles.pessoaCell}>
                    <span>{value} anos {value < 18 && <span className={styles.menorIdadeBadge}>⚠️ Menor</span>}</span>
                </div>
            ),
        },
    ];

    const handleClearFilters = () => {
        setFilterNome('');
        setFilterFaixa('todas');
    };

    const handleEdit = (pessoa: Pessoa) => {
        setEditingPessoa(pessoa);
        openModal();
    };

    const handleDelete = async (pessoa: Pessoa) => {
        const result = await Swal.fire({
            icon: 'warning',
            title: 'Tem certeza?',
            text: `Tem certeza que deseja excluir ${pessoa.nome}?`,
            showCancelButton: true,
            confirmButtonText: 'Sim, excluir',
            cancelButtonText: 'Cancelar',
        });

        if (result.isConfirmed) {
            const response = await deletePessoa(pessoa.id);
            if (!response?.sucesso) {
                Swal.fire({
                    icon: 'info',
                    title: 'Informação',
                    text: response?.mensagem ?? 'Não foi possível excluir a pessoa',
                });
            }
        }
    };

    const handleSubmit = async (data: PessoaInput) => {
        const response = editingPessoa
            ? await updatePessoa(editingPessoa.id, data)
            : await createPessoa(data);

        if (response?.sucesso) {
            closeModal();
            setEditingPessoa(null);
        } else {
            Swal.fire({
                icon: 'info',
                title: 'Informação',
                text: response?.mensagem ?? 'Não foi possível salvar a pessoa',
            });
        }
    };

    const handleAddNew = () => {
        setEditingPessoa(null);
        openModal();
    };

    if (loading && pessoas.length === 0) {
        return (
            <div className={styles.container}>
                <div className={styles.loadingContainer}>
                    <div className={styles.spinner} />
                    <p>Carregando pessoas...</p>
                </div>
            </div>
        );
    }

    if (error) {
        return (
            <div className={styles.container}>
                <div className={styles.errorContainer}>
                    <div className={styles.errorIcon}>⚠️</div>
                    <h3>Erro ao carregar pessoas</h3>
                    <p>{error}</p>
                </div>
            </div>
        );
    }

    return (
        <div className={styles.container}>
            <div className={styles.header}>
                <div>
                    <h1 className={styles.title}>Pessoas</h1>
                    <p className={styles.subtitle}>Cadastro de pessoas vinculadas às transações e relatórios</p>
                </div>
                <div className={styles.headerActions}>
                    <button type="button" onClick={handleAddNew} className={styles.addButton}>
                        + Nova Pessoa
                    </button>
                </div>
            </div>

            <div className={styles.filtersCard}>
                <div className={styles.filtersHeader}>
                    <h3 className={styles.filtersTitle}>🔍 Filtros</h3>
                    <button type="button" onClick={handleClearFilters} className={styles.clearFilters}>
                        Limpar Filtros
                    </button>
                </div>
                <div className={styles.filtersGrid}>
                    <div className={styles.filterGroup}>
                        <label className={styles.filterLabel}>Buscar por nome</label>
                        <input
                            type="search"
                            value={filterNome}
                            onChange={e => setFilterNome(e.target.value)}
                            placeholder="Digite parte do nome..."
                            className={styles.filterInput}
                        />
                    </div>
                    <div className={styles.filterGroup}>
                        <label className={styles.filterLabel}>Faixa etária</label>
                        <select
                            value={filterFaixa}
                            onChange={e => setFilterFaixa(e.target.value as FaixaIdade)}
                            className={styles.filterSelect}
                        >
                            <option value="todas">Todas</option>
                            <option value="menor">Menores de 18 anos</option>
                            <option value="adulto">18 anos ou mais</option>
                        </select>
                    </div>
                </div>
            </div>

            <div className={styles.tableContainer}>
                {filteredPessoas.length === 0 ? (
                    <div className={styles.emptyState}>
                        <div className={styles.emptyIcon}>👥</div>
                        <h3>Nenhuma pessoa encontrada</h3>
                        <p>Não há registros ou os filtros não retornaram resultados.</p>
                    </div>
                ) : (
                    <>
                        <div className={styles.tableHeader}>
                            <div className={styles.tableTitle}>
                                <span>📋 Lista de Pessoas</span>
                                <span className={styles.tableCount}>{filteredPessoas.length} registros</span>
                            </div>
                        </div>
                        <DataTable
                            columns={columns}
                            data={filteredPessoas}
                            onEdit={handleEdit}
                            onDelete={handleDelete}
                        />
                    </>
                )}
            </div>

            <Modal isOpen={isOpen} onClose={closeModal} title={editingPessoa ? 'Editar Pessoa' : 'Nova Pessoa'} closeOnBackdropClick={false} closeOnEsc={false}>
                <PessoaForm
                    initialData={editingPessoa || undefined}
                    onSubmit={handleSubmit}
                    onCancel={closeModal}
                />
            </Modal>
        </div>
    );
};
