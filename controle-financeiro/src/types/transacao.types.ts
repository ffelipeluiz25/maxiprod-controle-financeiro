import type { Categoria, TipoTransacao } from "./categoria.types";
import type { Pessoa } from "./pessoa.types";

export interface Transacao {
    id: number;
    descricao: string;
    valor: number;
    tipo: TipoTransacao;
    idCategoria: number;
    idPessoa: number;
    categoria?: Categoria;
    pessoa?: Pessoa;
}

export interface TransacaoInput {
    descricao: string;
    valor: number;
    tipo: TipoTransacao;
    idCategoria: number;
    idPessoa: number;
}