import React, { useState, useEffect } from 'react';
import Swal from 'sweetalert2';
import type { TransacaoInput } from '../../../types/transacao.types';
import { usePessoas } from '../../../hooks/usePessoas';
import {
    FinalidadeCategoria,
    TipoTransacaoValor,
    type Categoria,
    type TipoTransacao,
} from '../../../types/categoria.types';
import type { Pessoa } from '../../../types/pessoa.types';
import { useCategorias } from '../../../hooks/useCategorias';

interface TransacaoFormProps {
    onSubmit: (data: TransacaoInput) => Promise<void>;
    onCancel: () => void;
}

export const TransacaoForm: React.FC<TransacaoFormProps> = ({ onSubmit, onCancel }) => {
    const { pessoas } = usePessoas();
    const {  getCategoriasByFinalidade } = useCategorias();

    const [formData, setFormData] = useState<TransacaoInput>({
        descricao: '',
        valor: NaN,
        tipo: TipoTransacaoValor.Despesa,
        idCategoria: 0,
        idPessoa: 0,
    });

    const [submitting, setSubmitting] = useState(false);
    const [valorText, setValorText] = useState('');
    const [pessoaSelecionada, setPessoaSelecionada] = useState<Pessoa | null>(null);
    const [categoriasDisponiveis, setCategoriasDisponiveis] = useState<Categoria[]>([]);

    const formatCurrencyValue = (value: number) => {
        return new Intl.NumberFormat('pt-BR', {
            minimumFractionDigits: 2,
            maximumFractionDigits: 2,
        }).format(value);
    };

    const normalizeCurrencyInput = (value: string) => {
        const onlyDigitsAndSeparators = value
            .replace(/\s/g, '')
            .replace(/,/g, '.')
            .replace(/[^\d.]/g, '');

        const dotCount = (onlyDigitsAndSeparators.match(/\./g) || []).length;
        if (dotCount <= 1) {
            return onlyDigitsAndSeparators;
        }

        const lastDotIndex = onlyDigitsAndSeparators.lastIndexOf('.');
        const integerPart = onlyDigitsAndSeparators
            .slice(0, lastDotIndex)
            .replace(/\./g, '');
        const decimalPart = onlyDigitsAndSeparators.slice(lastDotIndex + 1);

        return `${integerPart}.${decimalPart}`;
    };

    const parseCurrencyValue = (value: string) => {
        const normalized = normalizeCurrencyInput(value);
        if (!normalized || normalized === '.') {
            return NaN;
        }

        const [integerPart, decimalPart] = normalized.split('.');
        if ((integerPart.replace(/^0+/, '') || '0').length > 16) {
            return NaN;
        }

        if (decimalPart && decimalPart.length > 2) {
            return NaN;
        }

        const parsed = parseFloat(normalized);
        return Number.isFinite(parsed) ? parsed : NaN;
    };

    useEffect(() => {
        if (formData.tipo) {
            const categoriasFiltradas = getCategoriasByFinalidade(formData.tipo);
            setCategoriasDisponiveis(categoriasFiltradas);

            if (!categoriasFiltradas.find(c => c.id === formData.idCategoria)) {
                setFormData(prev => ({ ...prev, idCategoria: 0 }));
            }
        }
    }, [formData.tipo, getCategoriasByFinalidade]);

    useEffect(() => {
        const pessoa = pessoas.find(p => p.id === formData.idPessoa);
        setPessoaSelecionada(pessoa || null);
    }, [formData.idPessoa, pessoas]);

    const validate = (): boolean => {
        const messages: string[] = [];

        if (!formData.descricao.trim()) {
            messages.push('Descrição é obrigatória');
        } else if (formData.descricao.length > 400) {
            messages.push('Descrição deve ter no máximo 400 caracteres');
        }

        if (!formData.valor || formData.valor <= 0) {
            messages.push('Valor deve ser maior que zero');
        }

        if (formData.idPessoa === 0) {
            messages.push('Pessoa é obrigatória');
        }

        if (formData.idCategoria === 0) {
            messages.push('Categoria é obrigatória');
        }

        const pessoa = pessoas.find(p => p.id === formData.idPessoa);
        if (pessoa && pessoa.idade < 18 && formData.tipo === TipoTransacaoValor.Receita) {
            messages.push('Menores de idade só podem registrar despesas');
        }

        if (messages.length > 0) {
            Swal.fire({
                icon: 'info',
                title: 'Validação',
                html: messages.map(msg => `• ${msg}`).join('<br/>'),
            });
            return false;
        }

        return true;
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!validate()) return;

        setSubmitting(true);
        try {
            await onSubmit(formData);
            
            setFormData({
                descricao: '',
                valor: NaN,
                tipo: TipoTransacaoValor.Despesa,
                idCategoria: 0,
                idPessoa: 0,
            });
            setValorText('');
        } catch (error) {
            console.error('Erro ao salvar:', error);
        } finally {
            setSubmitting(false);
        }
    };

    return (
        <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '15px' }}>
            <div>
                <label style={{ display: 'block', marginBottom: '5px', fontWeight: 'bold' }}>
                    Descrição *
                </label>
                <textarea
                    value={formData.descricao}
                    onChange={(e) => setFormData({ ...formData, descricao: e.target.value })}
                    maxLength={400}
                    rows={2}
                    style={{
                        width: '96%',
                        padding: '8px',
                        borderRadius: '4px',
                        border: '1px solid #ddd',
                        fontSize: '14px'
                    }}
                />
            </div>

            <div>
                <label style={{ display: 'block', marginBottom: '5px', fontWeight: 'bold' }}>
                    Valor *
                </label>
                <input
                    type="text"
                    inputMode="decimal"
                    placeholder="0,00"
                    value={valorText}
                    onChange={(e) => {
                        const nextText = e.target.value;
                        const normalized = normalizeCurrencyInput(nextText);
                        const [integerPart, decimalPart] = normalized.split('.');
                        if ((integerPart.replace(/^0+/, '') || '0').length > 16) {
                            return;
                        }
                        if (decimalPart && decimalPart.length > 2) {
                            return;
                        }

                        setValorText(nextText);
                        setFormData({ ...formData, valor: parseCurrencyValue(nextText) });
                    }}
                    onBlur={() => {
                        if (!Number.isNaN(formData.valor) && formData.valor > 0) {
                            setValorText(formatCurrencyValue(formData.valor));
                        }
                    }}
                    onFocus={() => {
                        if (!Number.isNaN(formData.valor) && formData.valor > 0) {
                            setValorText(formData.valor.toFixed(2).replace('.', ','));
                        }
                    }}
                    style={{
                        width: '96%',
                        padding: '8px',
                        borderRadius: '4px',
                        border: '1px solid #ddd',
                        fontSize: '14px'
                    }}
                />
            </div>

            <div>
                <label style={{ display: 'block', marginBottom: '5px', fontWeight: 'bold' }}>
                    Tipo *
                </label>
                <select
                    value={formData.tipo}
                    onChange={(e) => setFormData({ ...formData, tipo: parseInt(e.target.value) as TipoTransacao })}
                    style={{
                        width: '100%',
                        padding: '8px',
                        borderRadius: '4px',
                        border: '1px solid #ddd',
                        fontSize: '14px'
                    }}
                >
                    <option value={TipoTransacaoValor.Despesa}>Despesa</option>
                    <option value={TipoTransacaoValor.Receita}>Receita</option>
                </select>
                {pessoaSelecionada &&
                    pessoaSelecionada.idade < 18 &&
                    formData.tipo === TipoTransacaoValor.Receita && (
                        <span style={{ color: '#ff9800', fontSize: '12px', display: 'block', marginTop: '5px' }}>
                            ⚠️ {pessoaSelecionada.nome} é menor de idade; apenas despesas são permitidas.
                        </span>
                    )}
            </div>

            <div>
                <label style={{ display: 'block', marginBottom: '5px', fontWeight: 'bold' }}>
                    Pessoa *
                </label>
                <select
                    value={formData.idPessoa}
                    onChange={(e) => setFormData({ ...formData, idPessoa: parseInt(e.target.value) })}
                    style={{
                        width: '100%',
                        padding: '8px',
                        borderRadius: '4px',
                        border: '1px solid #ddd',
                        fontSize: '14px'
                    }}
                >
                    <option value={0}>Selecione uma pessoa</option>
                    {pessoas.map(pessoa => (
                        <option key={pessoa.id} value={pessoa.id}>
                            {pessoa.nome} ({pessoa.idade} anos)
                        </option>
                    ))}
                </select>
            </div>

            <div>
                <label style={{ display: 'block', marginBottom: '5px', fontWeight: 'bold' }}>
                    Categoria *
                </label>
                <select
                    value={formData.idCategoria}
                    onChange={(e) => setFormData({ ...formData, idCategoria: parseInt(e.target.value) })}
                    style={{
                        width: '100%',
                        padding: '8px',
                        borderRadius: '4px',
                        border: '1px solid #ddd',
                        fontSize: '14px'
                    }}
                >
                    <option value={0}>Selecione uma categoria</option>
                    {categoriasDisponiveis.map(categoria => (
                        <option key={categoria.id} value={categoria.id}>
                            {categoria.descricao} (
                            {categoria.finalidade === FinalidadeCategoria.Ambas
                                ? 'Ambas'
                                : categoria.finalidade}
                            )
                        </option>
                    ))}
                </select>
                {categoriasDisponiveis.length === 0 && (
                    <span style={{ color: '#ff9800', fontSize: '12px', display: 'block', marginTop: '5px' }}>
                        ⚠️ Nenhuma categoria disponível para{' '}
                        {formData.tipo === TipoTransacaoValor.Receita ? 'receitas' : 'despesas'}. Cadastre uma categoria
                        com finalidade adequada.
                    </span>
                )}
            </div>

            <div style={{ display: 'flex', gap: '10px', justifyContent: 'flex-end', marginTop: '10px' }}>
                <button
                    type="button"
                    onClick={onCancel}
                    style={{
                        padding: '8px 16px',
                        backgroundColor: '#f44336',
                        color: 'white',
                        border: 'none',
                        borderRadius: '4px',
                        cursor: 'pointer'
                    }}
                >
                    Cancelar
                </button>
                <button
                    type="submit"
                    disabled={submitting || categoriasDisponiveis.length === 0 || Boolean(pessoaSelecionada && pessoaSelecionada.idade < 18 && formData.tipo === TipoTransacaoValor.Receita)}
                    style={{
                        padding: '8px 16px',
                        backgroundColor: '#4CAF50',
                        color: 'white',
                        border: 'none',
                        borderRadius: '4px',
                        cursor:
                            submitting || categoriasDisponiveis.length === 0 || Boolean(pessoaSelecionada && pessoaSelecionada.idade < 18 && formData.tipo === TipoTransacaoValor.Receita)
                                ? 'not-allowed'
                                : 'pointer',
                    }}
                >
                    {submitting ? 'Salvando...' : 'Salvar'}
                </button>
            </div>
        </form>
    );
};