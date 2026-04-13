using FinanceApp.Application.DTOs;
using FinanceApp.Application.Features.Categorias.Commands;
using FinanceApp.Application.Features.Pessoas.Commands;
using FinanceApp.Application.Repository;
using FinanceApp.Domain.Entidades;
using FluentValidation;
using MediatR;
namespace FinanceApp.Application.Features.Categorias.Handlers
{
    public class CriarCategoriaHandler : IRequestHandler<CriarCategoriaCommand, RetornoDTO<int>>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IValidator<CriarCategoriaCommand> _validator;

        public CriarCategoriaHandler(ICategoriaRepository categoriaRepository,
                                     IValidator<CriarCategoriaCommand> validator)
        {
            _validator = validator;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<RetornoDTO<int>> Handle(CriarCategoriaCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errorMessage = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                return new RetornoDTO<int>(false, errorMessage);
            }

            var categoria = new CategoriaEntity
            {
                Descricao = request.Descricao,
                Finalidade = request.EnumFinalidade,
                DataCriacao = DateTime.Now
            };

            return await _categoriaRepository.Add(categoria);
        }

    }
}