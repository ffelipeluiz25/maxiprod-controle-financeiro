export type Finalidade = 1 | 2 | 3;
export type TipoTransacao = 1 | 2;

export const FinalidadeCategoria = {
    Despesa: 1 as Finalidade,
    Receita: 2 as Finalidade,
    Ambas: 3 as Finalidade,
} as const;

export const TipoTransacaoValor = {
    Despesa: 1 as TipoTransacao,
    Receita: 2 as TipoTransacao,
} as const;

export interface Categoria {
    id: number;
    descricao: string;
    finalidade: Finalidade;
}

export interface CategoriaInput {
    descricao: string;
    finalidade: Finalidade;
}