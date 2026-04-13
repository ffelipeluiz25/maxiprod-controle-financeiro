export interface RetornoDTO<T = unknown> {
    sucesso: boolean;
    mensagem?: string | null;
    idMensagem?: string | null;
    dados?: T;
}

export function dadosRetorno<T>(body: RetornoDTO<T>): T {
    if (!body?.sucesso) {
        throw new Error(body?.mensagem ?? 'Operação não concluída');
    }
    if (body.dados === undefined || body.dados === null) {
        throw new Error(body?.mensagem ?? 'Resposta sem dados');
    }
    return body.dados;
}

export function dadosLista<T>(body: RetornoDTO<T[]>): T[] {
    const list = dadosRetorno(body);
    if (!Array.isArray(list)) {
        throw new Error('Resposta de listagem inválida: dados não é um array');
    }
    return list;
}
