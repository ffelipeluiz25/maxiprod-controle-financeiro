using AutoMapper;
using FinanceApp.Application.DTOs;
using FinanceApp.Application.DTOs.Categoria;
using FinanceApp.Application.DTOs.Pessoa;
using FinanceApp.Application.DTOs.Pessoa.Request;
using FinanceApp.Application.DTOs.Transacao;
using FinanceApp.Domain.Entidades;
namespace FinanceApp.Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PessoaDTO, PessoaEntity>().ReverseMap();
            CreateMap<CategoriaDTO, CategoriaEntity>().ReverseMap();
            CreateMap<TransacaoDTO, TransacaoEntity>().ReverseMap();

            CreateMap<PessoaDTO, CriarPessoaDTO>().ReverseMap();
            CreateMap<PessoaDTO, AtualizarPessoaDTO>().ReverseMap();
            CreateMap<RetornoDTO, RetornoDTO>().ReverseMap();
        }
    }
}