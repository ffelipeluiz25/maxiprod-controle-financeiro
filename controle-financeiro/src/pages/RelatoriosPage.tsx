import React, { useEffect, useState } from 'react';
import styles from '../styles/ListPageLayout.module.css';
import { useRelatorios } from '../hooks/useRelatorios';
import type { TotaisPorPessoa, TotaisPorCategoria, TotalGeral } from '../types/relatorio.types';

export const RelatoriosPage: React.FC = () => {
    const { getTotaisPorPessoa, getTotaisPorCategoria, loading } = useRelatorios();
    const [relatorioPessoa, setRelatorioPessoa] = useState<TotaisPorPessoa[]>([]);
    const [relatorioCategoria, setRelatorioCategoria] = useState<TotaisPorCategoria[]>([]);
    const [totaisGeraisPessoa, setTotaisGeraisPessoa] = useState<TotalGeral | null>(null);
    const [totaisGeraisCategoria, setTotaisGeraisCategoria] = useState<TotalGeral | null>(null);
    const [activeTab, setActiveTab] = useState<'pessoa' | 'categoria'>('pessoa');

    useEffect(() => {
        void loadRelatorios();
    }, []);

    const loadRelatorios = async () => {
        const pessoaData = await getTotaisPorPessoa();
        if (pessoaData) {
            setRelatorioPessoa(pessoaData.pessoas);
            setTotaisGeraisPessoa(pessoaData.totaisGerais);
        }

        const categoriaData = await getTotaisPorCategoria();
        if (categoriaData) {
            setRelatorioCategoria(categoriaData.categorias);
            setTotaisGeraisCategoria(categoriaData.totaisGerais);
        }
    };

    const formatCurrency = (value: number) => {
        return new Intl.NumberFormat('pt-BR', {
            style: 'currency',
            currency: 'BRL',
        }).format(value);
    };

    if (loading && relatorioPessoa.length === 0 && relatorioCategoria.length === 0) {
        return (
            <div className={styles.container}>
                <div className={styles.loadingContainer}>
                    <div className={styles.spinner} />
                    <p>Carregando relatórios...</p>
                </div>
            </div>
        );
    }

    return (
        <div className={styles.container}>
            <div className={styles.header}>
                <div>
                    <h1 className={styles.title}>Relatórios</h1>
                    <p className={styles.subtitle}>Análise financeira por pessoa e por categoria</p>
                </div>
            </div>

            <div className={styles.tabsRow}>
                <button
                    type="button"
                    className={activeTab === 'pessoa' ? styles.tabButtonActive : styles.tabButton}
                    onClick={() => setActiveTab('pessoa')}
                >
                    👥 Totais por Pessoa
                </button>
                <button
                    type="button"
                    className={activeTab === 'categoria' ? styles.tabButtonActive : styles.tabButton}
                    onClick={() => setActiveTab('categoria')}
                >
                    🏷️ Totais por Categoria
                </button>
            </div>

            {activeTab === 'pessoa' && (
                <div className={styles.tableContainer}>
                    <div className={styles.tableHeader}>
                        <div className={styles.tableTitle}>
                            <span>👥 Por pessoa</span>
                            <span className={styles.tableCount}>{relatorioPessoa.length} registros</span>
                        </div>
                    </div>
                    <div className={styles.innerTableWrap}>
                        <table className={styles.reportTable}>
                            <thead>
                                <tr>
                                    <th>Pessoa</th>
                                    <th>Total Receitas</th>
                                    <th>Total Despesas</th>
                                    <th>Saldo</th>
                                </tr>
                            </thead>
                            <tbody>
                                {relatorioPessoa.length === 0 ? (
                                    <tr>
                                        <td colSpan={4} className={styles.emptyRow}>
                                            Nenhum dado encontrado
                                        </td>
                                    </tr>
                                ) : (
                                    relatorioPessoa.map(item => (
                                        <tr key={item.id}>
                                            <td className={styles.nameCell}>{item.nome}</td>
                                            <td className={styles.cellReceita}>{formatCurrency(item.totalReceitas)}</td>
                                            <td className={styles.cellDespesa}>{formatCurrency(item.totalDespesas)}</td>
                                            <td className={item.saldo >= 0 ? styles.cellReceita : styles.cellDespesa}>
                                                {formatCurrency(item.saldo)}
                                            </td>
                                        </tr>
                                    ))
                                )}
                            </tbody>
                            {totaisGeraisPessoa && (
                                <tfoot>
                                    <tr className={styles.totalRow}>
                                        <td>
                                            <strong>TOTAL GERAL</strong>
                                        </td>
                                        <td>
                                            <strong>{formatCurrency(totaisGeraisPessoa.receita)}</strong>
                                        </td>
                                        <td>
                                            <strong>{formatCurrency(totaisGeraisPessoa.despesa)}</strong>
                                        </td>
                                        <td>
                                            <strong
                                                className={
                                                    totaisGeraisPessoa.saldo >= 0
                                                        ? styles.cellReceita
                                                        : styles.cellDespesa
                                                }
                                            >
                                                {formatCurrency(totaisGeraisPessoa.saldo)}
                                            </strong>
                                        </td>
                                    </tr>
                                </tfoot>
                            )}
                        </table>
                    </div>
                </div>
            )}

            {activeTab === 'categoria' && (
                <div className={styles.tableContainer}>
                    <div className={styles.tableHeader}>
                        <div className={styles.tableTitle}>
                            <span>🏷️ Por categoria</span>
                            <span className={styles.tableCount}>{relatorioCategoria.length} registros</span>
                        </div>
                    </div>
                    <div className={styles.innerTableWrap}>
                        <table className={styles.reportTable}>
                            <thead>
                                <tr>
                                    <th>Categoria</th>
                                    <th>Total Receitas</th>
                                    <th>Total Despesas</th>
                                    <th>Saldo</th>
                                </tr>
                            </thead>
                            <tbody>
                                {relatorioCategoria.length === 0 ? (
                                    <tr>
                                        <td colSpan={4} className={styles.emptyRow}>
                                            Nenhum dado encontrado
                                        </td>
                                    </tr>
                                ) : (
                                    relatorioCategoria.map(item => (
                                        <tr key={item.id}>
                                            <td className={styles.nameCell}>{item.descricao}</td>
                                            <td className={styles.cellReceita}>{formatCurrency(item.totalReceitas)}</td>
                                            <td className={styles.cellDespesa}>{formatCurrency(item.totalDespesas)}</td>
                                            <td className={item.saldo >= 0 ? styles.cellReceita : styles.cellDespesa}>
                                                {formatCurrency(item.saldo)}
                                            </td>
                                        </tr>
                                    ))
                                )}
                            </tbody>
                            {totaisGeraisCategoria && (
                                <tfoot>
                                    <tr className={styles.totalRow}>
                                        <td>
                                            <strong>TOTAL GERAL</strong>
                                        </td>
                                        <td>
                                            <strong>{formatCurrency(totaisGeraisCategoria.receita)}</strong>
                                        </td>
                                        <td>
                                            <strong>{formatCurrency(totaisGeraisCategoria.despesa)}</strong>
                                        </td>
                                        <td>
                                            <strong
                                                className={
                                                    totaisGeraisCategoria.saldo >= 0
                                                        ? styles.cellReceita
                                                        : styles.cellDespesa
                                                }
                                            >
                                                {formatCurrency(totaisGeraisCategoria.saldo)}
                                            </strong>
                                        </td>
                                    </tr>
                                </tfoot>
                            )}
                        </table>
                    </div>
                </div>
            )}
        </div>
    );
};
