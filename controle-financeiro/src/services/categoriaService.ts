import type { Categoria, CategoriaInput } from '../types/categoria.types';
import type { RetornoDTO } from '../types/retorno.types';
import { api } from './api';

export const categoriaService = {
    async getAll(): Promise<RetornoDTO<Categoria[]>> {
        const { data } = await api.get<RetornoDTO<Categoria[]>>('/categorias');
        return data;
    },

    async getById(id: number): Promise<RetornoDTO<Categoria>> {
        const { data } = await api.get<RetornoDTO<Categoria>>(`/categorias/${id}`);
        return data;
    },

    async create(data: CategoriaInput): Promise<RetornoDTO<number>> {
        const { data: body } = await api.post<RetornoDTO<number>>('/categorias', data);
        return body;
    },
};
