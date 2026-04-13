import { useState, useEffect, useCallback } from 'react';
import Swal from 'sweetalert2';
import type { Pessoa, PessoaInput } from '../types/pessoa.types';
import type { RetornoDTO } from '../types/retorno.types';
import { pessoaService } from '../services/pessoaService';

export const usePessoas = () => {
    const [pessoas, setPessoas] = useState<Pessoa[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const loadPessoas = useCallback(async () => {
        setLoading(true);
        setError(null);
        try {
            const response = await pessoaService.getAll();
            if (response.sucesso) {
                setPessoas(response.dados ?? []);
            } else {
                Swal.fire({
                    icon: 'info',
                    title: 'Informação',
                    text: response.mensagem ?? 'Não foi possível carregar pessoas',
                });
            }
        } catch (err) {
            setError('Erro ao carregar pessoas');
            console.error(err);
        } finally {
            setLoading(false);
        }
    }, []);

    const createPessoa = useCallback(async (data: PessoaInput) => {
        setLoading(true);
        setError(null);
        try {
            const response = await pessoaService.create(data);
            const id = response.sucesso && typeof response.dados === 'number' ? response.dados : undefined;
            if (id !== undefined) {
                setPessoas(prev => [...prev, { id, ...data }]);
            }
            return response;
        } catch (err) {
            setError('Erro ao criar pessoa');
            throw err;
        } finally {
            setLoading(false);
        }
    }, []);

    const updatePessoa = useCallback(async (id: number, data: PessoaInput) => {
        setLoading(true);
        setError(null);
        try {
            const response = await pessoaService.update(id, data);
            if (response.sucesso) {
                setPessoas(prev => prev.map(p => p.id === id ? { id, ...data } : p));
            }
            return response;
        } catch (err) {
            setError('Erro ao atualizar pessoa');
            throw err;
        } finally {
            setLoading(false);
        }
    }, []);

    const deletePessoa = useCallback(async (id: number) => {
        setLoading(true);
        setError(null);
        try {
            const response = await pessoaService.delete(id);
            if (response.sucesso) {
                setPessoas(prev => prev.filter(p => p.id !== id));
            }
            return response;
        } catch (err) {
            setError('Erro ao deletar pessoa');
            throw err;
        } finally {
            setLoading(false);
        }
    }, []);

    useEffect(() => {
        loadPessoas();
    }, [loadPessoas]);

    return {
        pessoas,
        loading,
        error,
        loadPessoas,
        createPessoa,
        updatePessoa,
        deletePessoa,
    };
};
