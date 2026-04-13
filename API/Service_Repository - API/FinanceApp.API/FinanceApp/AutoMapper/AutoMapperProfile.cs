using AutoMapper;
using FinanceApp.DTOs.Categoria;
using FinanceApp.DTOs.Pessoa;
using FinanceApp.DTOs.Pessoa.Request;
using FinanceApp.DTOs.Transacao;
using FinanceApp.Entity;
namespace FinanceApp.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PessoaDTO, PessoaEntity>().ReverseMap();
            CreateMap<CategoriaDTO, CategoriaEntity>().ReverseMap();
            CreateMap<TransacaoEntity, TransacaoDTO>().ReverseMap()
                    .ForMember(dest => dest.Pessoa, opt => opt.Ignore());

            CreateMap<PessoaDTO, CriarPessoaDTO>().ReverseMap();
            CreateMap<PessoaDTO, AtualizarPessoaDTO>().ReverseMap();
        }
    }
}