using FinanceApp.Application.DTOs;
using FinanceApp.Application.Features.Transacoes.Commands;
using FinanceApp.Application.Repository;
using FinanceApp.Domain.Entidades;
using FinanceApp.Domain.Enumeradores;
using FluentValidation;
using MediatR;
namespace FinanceApp.Application.Features.Transacoes.Handlers
{
    public class CriarTransacaoHandler : IRequestHandler<CriarTransacaoCommand, RetornoDTO<int>>
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IValidator<CriarTransacaoCommand> _validator;

        public CriarTransacaoHandler(IPessoaRepository pessoaRepository,
                                     ICategoriaRepository categoriaRepository,
                                     ITransacaoRepository transacaoRepository,
                                     IValidator<CriarTransacaoCommand> validator)
        {
            _validator = validator;
            _pessoaRepository = pessoaRepository;
            _categoriaRepository = categoriaRepository;
            _transacaoRepository = transacaoRepository;
        }

        public async Task<RetornoDTO<int>> Handle(CriarTransacaoCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errorMessage = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                return new RetornoDTO<int>(false, errorMessage);
            }

            var retornoPessoa = await _pessoaRepository.ObterPorId(request.IdPessoa);
            var retornoCategoria = await _categoriaRepository.ObterPorId(request.IdCategoria);

            if (!retornoPessoa.Sucesso || !retornoCategoria.Sucesso)
                return new RetornoDTO<int>(false, "Pessoa ou Categoria não encontrada");

            var transacao = new TransacaoEntity
            {
                Descricao = request.Descricao,
                Valor = request.Valor,
                Tipo = request.Tipo,
                IdCategoria = request.IdCategoria,
                IdPessoa = request.IdPessoa,
                DataCriacao = DateTime.UtcNow
            };

            return await _transacaoRepository.Add(transacao);

        }

    }
}