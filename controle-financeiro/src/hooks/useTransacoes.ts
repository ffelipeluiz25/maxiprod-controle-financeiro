import { useState, useEffect, useCallback } from 'react';
import Swal from 'sweetalert2';
import type { Transacao, TransacaoInput } from '../types/transacao.types';
import { transacaoService } from '../services/transacaoService';

export const useTransacoes = () => {
    const [transacoes, setTransacoes] = useState<Transacao[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const loadTransacoes = useCallback(async () => {
        setLoading(true);
        setError(null);
        try {
            const response = await transacaoService.getAll();
            if (response.sucesso) {
                setTransacoes(response.dados ?? []);
            } else {
                Swal.fire({
                    icon: 'info',
                    title: 'Informação',
                    text: response.mensagem ?? 'Não foi possível carregar transações',
                });
            }
        } catch (err) {
            setError('Erro ao carregar transações');
            console.error(err);
        } finally {
            setLoading(false);
        }
    }, []);

    const createTransacao = useCallback(async (data: TransacaoInput) => {
        setLoading(true);
        setError(null);
        try {
            const response = await transacaoService.create(data);
            const id = response.sucesso && typeof response.dados === 'number' ? response.dados : undefined;
            if (id !== undefined) {
                setTransacoes(prev => [...prev, { id, ...data }]);
            }
            return response;
        } catch (err) {
            setError('Erro ao criar transação');
            throw err;
        } finally {
            setLoading(false);
        }
    }, []);

    useEffect(() => {
        loadTransacoes();
    }, [loadTransacoes]);

    return {
        transacoes,
        loading,
        error,
        loadTransacoes,
        createTransacao,
    };
};