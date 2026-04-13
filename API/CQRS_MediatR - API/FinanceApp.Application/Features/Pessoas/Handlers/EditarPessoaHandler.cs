using FinanceApp.Application.DTOs;
using FinanceApp.Application.Features.Pessoas.Commands;
using FinanceApp.Application.Repository;
using FluentValidation;
using MediatR;
namespace FinanceApp.Application.Features.Pessoas.Handlers
{
    public class EditarPessoaHandler : IRequestHandler<EditarPessoaCommand, RetornoDTO<bool>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IValidator<EditarPessoaCommand> _validator;

        public EditarPessoaHandler(IPessoaRepository pessoaRepository,
                                   IValidator<EditarPessoaCommand> validator)
        {
            _validator = validator;
            _pessoaRepository = pessoaRepository;
        }

        public async Task<RetornoDTO<bool>> Handle(EditarPessoaCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errorMessage = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                return new RetornoDTO<bool>(false, errorMessage);
            }

            return await _pessoaRepository.Update(request);
        }

    }
}