using FinanceApp.Application.Features.Pessoas.Commands;
using FluentValidation;
namespace FinanceApp.Application.Features.Pessoas.Validator
{
   
    public class EditarPessoaCommandValidator : AbstractValidator<EditarPessoaCommand>
    {
        public EditarPessoaCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Identificador da pessoa é obrigatório")
                .GreaterThan(0).WithMessage("Identificador inválido");

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