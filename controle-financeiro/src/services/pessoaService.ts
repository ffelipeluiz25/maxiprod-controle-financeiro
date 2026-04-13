import type { Pessoa, PessoaInput } from '../types/pessoa.types';
import type { RetornoDTO } from '../types/retorno.types';
import { api } from './api';

export const pessoaService = {
    async getAll(): Promise<RetornoDTO<Pessoa[]>> {
        const { data } = await api.get<RetornoDTO<Pessoa[]>>('/pessoas');
        return data;
    },

    async getById(id: number): Promise<RetornoDTO<Pessoa>> {
        const { data } = await api.get<RetornoDTO<Pessoa>>(`/pessoas/${id}`);
        return data;
    },

    async create(data: PessoaInput): Promise<RetornoDTO<number>> {
        const { data: body } = await api.post<RetornoDTO<number>>('/pessoas', data);
        return body;
    },

    async update(id: number, data: PessoaInput): Promise<RetornoDTO<boolean>> {
        const { data: body } = await api.put<RetornoDTO<boolean>>(`/pessoas/${id}`, { ...data, id });
        return body;
    },

    async delete(id: number): Promise<RetornoDTO<boolean>> {
        const { data } = await api.delete<RetornoDTO<boolean>>(`/pessoas/${id}`);
        return data;
    },
};
