using FinanceApp.Application.DTOs;
using FinanceApp.Application.Features.Pessoas.Commands;
using FinanceApp.Application.Repository;
using FluentValidation;
using MediatR;
namespace FinanceApp.Application.Features.Pessoas.Handlers
{
    public class DeletarPessoaHandler : IRequestHandler<DeletarPessoaCommand, RetornoDTO<bool>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IValidator<DeletarPessoaCommand> _validator;

        public DeletarPessoaHandler(IPessoaRepository pessoaRepository,
                                    IValidator<DeletarPessoaCommand> validator)
        {
            _validator = validator;
            _pessoaRepository = pessoaRepository;
        }

        public async Task<RetornoDTO<bool>> Handle(DeletarPessoaCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errorMessage = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                return new RetornoDTO<bool>(false, errorMessage);
            }

            var retorno = await _pessoaRepository.ObterPorId(request.Id);
            if (!retorno.Sucesso)
                return new RetornoDTO<bool>(false, "Pessoa não encontrada!");

            return await _pessoaRepository.Delete(retorno.Dados.Id);
        }
    }
}