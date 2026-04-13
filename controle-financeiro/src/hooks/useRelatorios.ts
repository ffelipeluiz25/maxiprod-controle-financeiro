import { useState, useCallback } from 'react';
import Swal from 'sweetalert2';
import { relatorioService } from '../services/relatorioService';
import type { TotaisPorPessoa, TotalGeral, RelatorioPessoaResponse, TotaisPorCategoria, RelatorioCategoriaResponse } from '../types/relatorio.types';
export const useRelatorios = () => {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const getTotaisPorPessoa = useCallback(async (): Promise<{
        pessoas: TotaisPorPessoa[];
        totaisGerais: TotalGeral;
    } | null> => {
        setLoading(true);
        setError(null);
        try {
            const response = await relatorioService.getTotaisPorPessoa();
            if (!response.sucesso) {
                Swal.fire({
                    icon: 'info',
                    title: 'Informação',
                    text: response.mensagem ?? 'Não foi possível carregar relatório por pessoa',
                });
                return null;
            }
            const data = response.dados as RelatorioPessoaResponse;

            console.log('Dados processados de pessoas:', data);

            return {
                pessoas: data?.listaTotalPessoas || [],
                totaisGerais: data?.totalGeral || { receita: 0, despesa: 0, saldo: 0 }
            };
        } catch (err: any) {
            console.error('Erro ao carregar relatório por pessoa:', err);
            setError(err.message || 'Erro ao carregar relatório por pessoa');
            return null;
        } finally {
            setLoading(false);
        }
    }, []);

    const getTotaisPorCategoria = useCallback(async (): Promise<{
        categorias: TotaisPorCategoria[];
        totaisGerais: TotalGeral;
    } | null> => {
        setLoading(true);
        setError(null);
        try {
            const response = await relatorioService.getTotaisPorCategoria();
            if (!response.sucesso) {
                Swal.fire({
                    icon: 'info',
                    title: 'Informação',
                    text: response.mensagem ?? 'Não foi possível carregar relatório por categoria',
                });
                return null;
            }
            const data = response.dados as RelatorioCategoriaResponse;

            console.log('Dados processados de categorias:', data);

            return {
                categorias: data?.listaTotalCategorias || [],
                totaisGerais: data?.totalGeral || { receita: 0, despesa: 0, saldo: 0 }
            };
        } catch (err: any) {
            console.error('Erro ao carregar relatório por categoria:', err);
            setError(err.message || 'Erro ao carregar relatório por categoria');
            return null;
        } finally {
            setLoading(false);
        }
    }, []);

    return {
        loading,
        error,
        getTotaisPorPessoa,
        getTotaisPorCategoria,
    };
};