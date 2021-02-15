using AutoMapper;
using LR.Avaliacao.Application.Models.Cliente;
using LR.Avaliacao.Domain.Entities;
using LR.Avaliacao.Domain.EntitiesData;

namespace LR.Avaliacao.Application.Mapping
{
    public class ClienteMapper : Profile
    {
        public ClienteMapper()
        {
            CreateMap<ClienteModel, Cliente>()
                .ForMember(dest => dest.Cpf, m => m.Ignore())
                .ConstructUsing(src =>
                    new Cliente(src.Nome, new Domain.ValueObjects.Cpf(src.Cpf), src.Aniversario));

            CreateMap<Cliente, ClienteModel>()
                .ForMember(dest => dest.Cpf, m => m.MapFrom(src => src.Cpf.Valor));

            CreateMap<Cliente, ClienteData>()
                .ForMember(dest => dest.Cpf, m => m.MapFrom(src => src.Cpf.Valor));

            CreateMap<ClienteData, ClienteRetornoModel>();
        }
    }
}
