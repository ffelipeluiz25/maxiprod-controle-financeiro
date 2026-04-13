using FinanceApp.Application.Features.Pessoas.Commands;
using FluentValidation;
namespace FinanceApp.Application.Features.Pessoas.Validator
{
    public class CriarPessoaCommandValidator : AbstractValidator<CriarPessoaCommand>
    {
        public CriarPessoaCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MaximumLength(200).WithMessage("Nome deve ter no máximo 200 caracteres");

            RuleFor(x => x.Idade)
                .NotEmpty().WithMessage("Idade é obrigatória")
                .GreaterThan(0).WithMessage("Idade deve ser maior que zero")
                .LessThanOrEqualTo(120).WithMessage("Idade deve ser menor ou igual a 120");
        }
    }
}