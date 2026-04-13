using FinanceApp.Application.Features.Categorias.Commands;
using FluentValidation;
namespace FinanceApp.Application.Features.Categorias.Validator
{
    public class CriarCategoriaCommandValidator : AbstractValidator<CriarCategoriaCommand>
    {
        public CriarCategoriaCommandValidator()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Descrição é obrigatória")
                .MaximumLength(400).WithMessage("Descrição deve ter no máximo 400 caracteres");

            RuleFor(x => x.EnumFinalidade)
                .NotEmpty().WithMessage("Finalidade é obrigatória")
                .IsInEnum().WithMessage("Finalidade inválida. Valores permitidos: Despesa, Receita, Ambas");
        }
    }
}