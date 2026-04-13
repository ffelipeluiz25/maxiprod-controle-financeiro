import React, { useState, useEffect } from 'react';
import Swal from 'sweetalert2';
import type { PessoaInput } from '../../../types/pessoa.types';

interface PessoaFormProps {
    initialData?: { id?: number; nome: string; idade: number };
    onSubmit: (data: PessoaInput) => Promise<void>;
    onCancel: () => void;
}

export const PessoaForm: React.FC<PessoaFormProps> = ({ initialData, onSubmit, onCancel }) => {
    const [formData, setFormData] = useState<PessoaInput>({
        nome: '',
        idade: NaN,
    });
    const [submitting, setSubmitting] = useState(false);

    useEffect(() => {
        if (initialData) {
            setFormData({
                nome: initialData.nome,
                idade: initialData.idade,
            });
        } else {
            setFormData({
                nome: '',
                idade: NaN,
            });
        }
    }, [initialData]);

    const validate = (): boolean => {
        const messages: string[] = [];
        if (!formData.nome.trim()) {
            messages.push('Nome é obrigatório');
        } else if (formData.nome.length > 200) {
            messages.push('Nome deve ter no máximo 200 caracteres');
        }
        if (isNaN(formData.idade)) {
            messages.push('Idade é obrigatória');
        } else if (formData.idade <= 0) {
            messages.push('Idade deve ser maior que zero');
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
            onCancel();
        } catch (error) {
            console.error('Erro ao salvar:', error);
        } finally {
            setSubmitting(false);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label>Nome *</label>
                <input
                    type="text"
                    value={formData.nome}
                    onChange={(e) => setFormData({ ...formData, nome: e.target.value })}
                    maxLength={200}
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
                <label>Idade *</label>
                <input
                    type="number"
                    value={isNaN(formData.idade) ? '' : formData.idade}
                    min={1}
                    max={999}
                    inputMode="numeric"
                    onChange={(e) => {
                        const value = e.target.value;
                        if (value === '') {
                            setFormData({ ...formData, idade: NaN });
                            return;
                        }
                        if (value.length > 3) {
                            return;
                        }
                        setFormData({ ...formData, idade: parseInt(value, 10) });
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
                    style={{
                        padding: '8px 16px',
                        backgroundColor: '#4CAF50',
                        color: 'white',
                        border: 'none',
                        borderRadius: '4px',
                        cursor: 'pointer',
                    }}
                >
                    {submitting ? 'Salvando...' : 'Salvar'}
                </button>
            </div>


        </form>
    );
};