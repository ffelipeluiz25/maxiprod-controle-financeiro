import type { Transacao, TransacaoInput } from '../types/transacao.types';
import type { RetornoDTO } from '../types/retorno.types';
import { api } from './api';

export const transacaoService = {
    async getAll(): Promise<RetornoDTO<Transacao[]>> {
        const { data } = await api.get<RetornoDTO<Transacao[]>>('/transacoes');
        return data;
    },

    async getById(id: number): Promise<RetornoDTO<Transacao>> {
        const { data } = await api.get<RetornoDTO<Transacao>>(`/transacoes/${id}`);
        return data;
    },

    async create(data: TransacaoInput): Promise<RetornoDTO<number>> {
        const { data: body } = await api.post<RetornoDTO<number>>('/transacoes', data);
        return body;
    },
};
