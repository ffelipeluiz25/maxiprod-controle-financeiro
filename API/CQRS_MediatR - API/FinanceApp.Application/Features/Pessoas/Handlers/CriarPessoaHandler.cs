using FinanceApp.Application.DTOs;
using FinanceApp.Application.Features.Pessoas.Commands;
using FinanceApp.Application.Features.Pessoas.Validator;
using FinanceApp.Application.Features.Transacoes.Commands;
using FinanceApp.Application.Repository;
using FinanceApp.Domain.Entidades;
using FluentValidation;
using MediatR;
namespace FinanceApp.Application.Features.Pessoas.Handlers
{
    public class CriarPessoaHandler : IRequestHandler<CriarPessoaCommand, RetornoDTO<int>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IValidator<CriarPessoaCommand> _validator;

        public CriarPessoaHandler(IPessoaRepository pessoaRepository,
                                  IValidator<CriarPessoaCommand> validator)
        {
            _validator = validator;
            _pessoaRepository = pessoaRepository;
        }

        public async Task<RetornoDTO<int>> Handle(CriarPessoaCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errorMessage = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                return new RetornoDTO<int>(false, errorMessage);
            }

            var pessoa = new PessoaEntity
            {
                Nome = request.Nome,
                Idade = request.Idade,
                DataCriacao = DateTime.UtcNow
            };

            return await _pessoaRepository.Add(pessoa);
        }

    }
}