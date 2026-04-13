using FinanceApp.Application.Features.Pessoas.Commands;
using FluentValidation;
namespace FinanceApp.Application.Features.Pessoas.Validator
{
    public class DeletarPessoaCommandValidator : AbstractValidator<DeletarPessoaCommand>
    {
        public DeletarPessoaCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Identificador da pessoa é obrigatório")
                .GreaterThan(0).WithMessage("Identificador inválido");
        }
    }
}