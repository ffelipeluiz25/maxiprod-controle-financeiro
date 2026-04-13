import React, { useState } from 'react';
import Swal from 'sweetalert2';
import { FinalidadeCategoria, type CategoriaInput, type Finalidade } from '../../../types/categoria.types';

interface CategoriaFormProps {
  onSubmit: (data: CategoriaInput) => Promise<void>;
  onCancel: () => void;
}

export const CategoriaForm: React.FC<CategoriaFormProps> = ({ onSubmit, onCancel }) => {
  const [formData, setFormData] = useState<CategoriaInput>({
    descricao: '',
    finalidade: FinalidadeCategoria.Ambas,
  });
  const [submitting, setSubmitting] = useState(false);

  const validate = (): boolean => {
    const messages: string[] = [];
    if (!formData.descricao.trim()) {
      messages.push('Descrição é obrigatória');
    } else if (formData.descricao.length > 400) {
      messages.push('Descrição deve ter no máximo 400 caracteres');
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
      setFormData({ descricao: '', finalidade: 3 });
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
          rows={3}
          style={{
            width: '96%',
            padding: '8px',
            borderRadius: '4px',
            border: '1px solid #ddd',
            fontSize: '14px'
          }}
        />
        <span style={{ fontSize: '12px', color: '#666' }}>
          {formData.descricao.length}/400 caracteres
        </span>
      </div>

      <div>
        <label style={{ display: 'block', marginBottom: '5px', fontWeight: 'bold' }}>
          Finalidade *
        </label>
        <select
          value={formData.finalidade}
          onChange={(e) => setFormData({ ...formData, finalidade: parseInt(e.target.value) as Finalidade })}
          style={{
            width: '100%',
            padding: '8px',
            borderRadius: '4px',
            border: '1px solid #ddd',
            fontSize: '14px'
          }}
        >
          <option value={FinalidadeCategoria.Despesa}>Despesa</option>
          <option value={FinalidadeCategoria.Receita}>Receita</option>
          <option value={FinalidadeCategoria.Ambas}>Ambas</option>
        </select>
        <span style={{ fontSize: '12px', color: '#666' }}>
          Define se esta categoria pode ser usada para despesas, receitas ou ambos
        </span>
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
          disabled={submitting}
          style={{
            padding: '8px 16px',
            backgroundColor: '#4CAF50',
            color: 'white',
            border: 'none',
            borderRadius: '4px',
            cursor: submitting ? 'not-allowed' : 'pointer'
          }}
        >
          {submitting ? 'Salvando...' : 'Salvar'}
        </button>
      </div>
    </form>
  );
};