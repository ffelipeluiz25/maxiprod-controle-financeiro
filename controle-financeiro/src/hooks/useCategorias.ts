import { useState, useEffect, useCallback } from 'react';
import Swal from 'sweetalert2';
import { FinalidadeCategoria, type Categoria, type CategoriaInput, type Finalidade } from '../types/categoria.types';
import { categoriaService } from '../services/categoriaService';


export const useCategorias = () => {
    const [categorias, setCategorias] = useState<Categoria[]>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const loadCategorias = useCallback(async () => {
        setLoading(true);
        setError(null);
        try {
            const response = await categoriaService.getAll();
            if (response.sucesso) {
                setCategorias(response.dados ?? []);
            } else {
                Swal.fire({
                    icon: 'info',
                    title: 'Informação',
                    text: response.mensagem ?? 'Não foi possível carregar categorias',
                });
            }
        } catch (err) {
            setError('Erro ao carregar categorias');
            console.error(err);
        } finally {
            setLoading(false);
        }
    }, []);

    const createCategoria = useCallback(async (data: CategoriaInput) => {
        setLoading(true);
        setError(null);
        try {
            const response = await categoriaService.create(data);
            const id = response.sucesso && typeof response.dados === 'number' ? response.dados : undefined;
            if (id !== undefined) {
                setCategorias(prev => [...prev, { id, ...data }]);
            }
            return response;
        } catch (err) {
            setError('Erro ao criar categoria');
            throw err;
        } finally {
            setLoading(false);
        }
    }, []);

    const getCategoriasByFinalidade = useCallback((finalidade: Finalidade) => {
        return categorias.filter(c => c.finalidade === finalidade);
    }, [categorias]);

    useEffect(() => {
        loadCategorias();
    }, [loadCategorias]);

    return {
        categorias,
        loading,
        error,
        loadCategorias,
        createCategoria,
        getCategoriasByFinalidade,
    };
};