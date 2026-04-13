import type { RelatorioCategoriaResponse, RelatorioPessoaResponse } from '../types/relatorio.types';
import type { RetornoDTO } from '../types/retorno.types';
import { api } from './api';

export const relatorioService = {
    async getTotaisPorPessoa(): Promise<RetornoDTO<RelatorioPessoaResponse>> {
        const { data } = await api.get<RetornoDTO<RelatorioPessoaResponse>>('/relatorios/totais-por-pessoa');
        return data;
    },

    async getTotaisPorCategoria(): Promise<RetornoDTO<RelatorioCategoriaResponse>> {
        const { data } = await api.get<RetornoDTO<RelatorioCategoriaResponse>>('/relatorios/totais-por-categoria');
        return data;
    },
};
