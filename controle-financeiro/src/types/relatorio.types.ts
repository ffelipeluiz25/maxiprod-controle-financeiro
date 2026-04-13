export interface TotalGeral {
    receita: number;
    despesa: number;
    saldo: number;
}

export interface TotaisPorPessoa {
    id: number;
    nome: string;
    totalReceitas: number;
    totalDespesas: number;
    saldo: number;
}

export interface ListaTotalPessoas {
    listaTotalPessoas: TotaisPorPessoa[];
    totalGeral: TotalGeral;
}

export interface TotaisPorCategoria {
    id: number;
    descricao: string;
    totalReceitas: number;
    totalDespesas: number;
    saldo: number;
}

export interface ListaTotalCategorias {
    listaTotalCategorias: TotaisPorCategoria[];
    totalGeral: TotalGeral;
}

export type RelatorioPessoaResponse = ListaTotalPessoas;
export type RelatorioCategoriaResponse = ListaTotalCategorias;