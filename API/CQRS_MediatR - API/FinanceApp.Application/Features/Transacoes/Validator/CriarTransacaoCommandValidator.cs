using FinanceApp.Application.Features.Transacoes.Commands;
using FluentValidation;
namespace FinanceApp.Application.Features.Transacoes.Validator
{
    public class CriarTransacaoCommandValidator : AbstractValidator<CriarTransacaoCommand>
    {
        public CriarTransacaoCommandValidator()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Descrição é obrigatória")
                .MaximumLength(400).WithMessage("Descrição deve ter no máximo 400 caracteres");

            RuleFor(x => x.Valor)
                .NotEmpty().WithMessage("Valor é obrigatório")
                .GreaterThan(0).WithMessage("Valor deve ser maior que zero");

            RuleFor(x => x.Tipo)
                .NotEmpty().WithMessage("Tipo é obrigatório")
                .IsInEnum().WithMessage("Tipo inválido. Valores permitidos: Despesa, Receita");

            RuleFor(x => x.IdCategoria)
                .NotEmpty().WithMessage("Identificador da categoria é obrigatório")
                .GreaterThan(0).WithMessage("Identificador da categoria inválido");

            RuleFor(x => x.IdPessoa)
                .NotEmpty().WithMessage("Identificador da pessoa é obrigatório")
                .GreaterThan(0).WithMessage("Identificador da pessoa inválido");
        }
    }
}