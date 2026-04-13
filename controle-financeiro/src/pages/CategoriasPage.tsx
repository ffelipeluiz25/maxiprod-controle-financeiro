import React, { useMemo, useState } from 'react';
import Swal from 'sweetalert2';
import { useModal } from '../hooks/useModal';
import { FinalidadeCategoria, type CategoriaInput, type Finalidade } from '../types/categoria.types';
import { Modal } from '../components/common/Modal/Modal';
import { DataTable } from '../components/common/DataTable/DataTable';
import { CategoriaForm } from '../components/features/Categorias/CategoriaForm';
import { useCategorias } from '../hooks/useCategorias';
import styles from '../styles/ListPageLayout.module.css';

export const CategoriasPage: React.FC = () => {
    const { categorias, loading, error, createCategoria } = useCategorias();
    const { isOpen, openModal, closeModal } = useModal();
    const [filterFinalidade, setFilterFinalidade] = useState<Finalidade | 'todas'>('todas');
    const [filterDescricao, setFilterDescricao] = useState('');

    const filteredCategorias = useMemo(() => {
        return categorias.filter(c => {
            if (filterFinalidade !== 'todas' && c.finalidade !== filterFinalidade) return false;
            const q = filterDescricao.trim().toLowerCase();
            if (q && !c.descricao.toLowerCase().includes(q)) return false;
            return true;
        });
    }, [categorias, filterFinalidade, filterDescricao]);

    const columns = [
        { key: 'descricao' as const, header: 'Descrição' },
        {
            key: 'finalidade' as const,
            header: 'Finalidade',
            render: (value: Finalidade) => {
                const labels = {
                    [FinalidadeCategoria.Despesa]: 'Despesa',
                    [FinalidadeCategoria.Receita]: 'Receita',
                    [FinalidadeCategoria.Ambas]: 'Ambas',
                } as Record<Finalidade, string>;
                const colors = {
                    [FinalidadeCategoria.Despesa]: '#e53935',
                    [FinalidadeCategoria.Receita]: '#43a047',
                    [FinalidadeCategoria.Ambas]: '#fb8c00',
                } as Record<Finalidade, string>;
                return (
                    <span
                        className={styles.categoriaBadge}
                        style={{ color: colors[value], fontWeight: 600, textTransform: 'uppercase' }}
                    >
                        {labels[value]}
                    </span>
                );
            },
        },
    ];

    const handleClearFilters = () => {
        setFilterFinalidade('todas');
        setFilterDescricao('');
    };

    const handleSubmit = async (data: CategoriaInput) => {
        const response = await createCategoria(data);
        if (response?.sucesso) {
            closeModal();
        } else {
            Swal.fire({
                icon: 'info',
                title: 'Informação',
                text: response?.mensagem ?? 'Não foi possível criar a categoria',
            });
        }
    };

    const handleAddNew = () => {
        openModal();
    };

    if (loading && categorias.length === 0) {
        return (
            <div className={styles.container}>
                <div className={styles.loadingContainer}>
                    <div className={styles.spinner} />
                    <p>Carregando categorias...</p>
                </div>
            </div>
        );
    }

    if (error) {
        return (
            <div className={styles.container}>
                <div className={styles.errorContainer}>
                    <div className={styles.errorIcon}>⚠️</div>
                    <h3>Erro ao carregar categorias</h3>
                    <p>{error}</p>
                </div>
            </div>
        );
    }

    return (
        <div className={styles.container}>
            <div className={styles.header}>
                <div>
                    <h1 className={styles.title}>Categorias</h1>
                    <p className={styles.subtitle}>Cadastre e organize categorias por finalidade (despesa, receita ou ambas)</p>
                </div>
                <div className={styles.headerActions}>
                    <button type="button" onClick={handleAddNew} className={styles.addButton}>
                        + Nova Categoria
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
                        <label className={styles.filterLabel}>Finalidade</label>
                        <select
                            value={filterFinalidade === 'todas' ? 'todas' : String(filterFinalidade)}
                            onChange={e => {
                                const v = e.target.value;
                                setFilterFinalidade(v === 'todas' ? 'todas' : (Number(v) as Finalidade));
                            }}
                            className={styles.filterSelect}
                        >
                            <option value="todas">Todas</option>
                            <option value={String(FinalidadeCategoria.Despesa)}>Despesa</option>
                            <option value={String(FinalidadeCategoria.Receita)}>Receita</option>
                            <option value={String(FinalidadeCategoria.Ambas)}>Ambas</option>
                        </select>
                    </div>
                    <div className={styles.filterGroup}>
                        <label className={styles.filterLabel}>Buscar na descrição</label>
                        <input
                            type="search"
                            value={filterDescricao}
                            onChange={e => setFilterDescricao(e.target.value)}
                            placeholder="Digite parte do nome..."
                            className={styles.filterInput}
                        />
                    </div>
                </div>
            </div>

            <div className={styles.tableContainer}>
                {filteredCategorias.length === 0 ? (
                    <div className={styles.emptyState}>
                        <div className={styles.emptyIcon}>📂</div>
                        <h3>Nenhuma categoria encontrada</h3>
                        <p>Ajuste os filtros ou cadastre uma nova categoria.</p>
                    </div>
                ) : (
                    <>
                        <div className={styles.tableHeader}>
                            <div className={styles.tableTitle}>
                                <span>📋 Lista de Categorias</span>
                                <span className={styles.tableCount}>{filteredCategorias.length} registros</span>
                            </div>
                        </div>
                        <DataTable columns={columns} data={filteredCategorias} actions={false} />
                    </>
                )}
            </div>

            <Modal isOpen={isOpen} onClose={closeModal} title="Nova Categoria" closeOnBackdropClick={false} closeOnEsc={false} >
                <CategoriaForm onSubmit={handleSubmit} onCancel={closeModal} />
            </Modal>
        </div>
    );
};
