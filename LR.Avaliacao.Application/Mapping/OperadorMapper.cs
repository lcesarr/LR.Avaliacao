using AutoMapper;
using LR.Avaliacao.Application.Models.Operador;
using LR.Avaliacao.Domain.Entities;
using LR.Avaliacao.Domain.EntitiesData;

namespace LR.Avaliacao.Application.Mapping
{
    public class OperadorMapper : Profile
    {
        public OperadorMapper()
        {
            CreateMap<OperadorModel, Operador>()
                .ForMember(dest => dest.Matricula, m => m.Ignore())
                .ConstructUsing(src =>
                    new Operador(src.Nome, new Domain.ValueObjects.Matricula(src.Matricula)));

            CreateMap<Operador, OperadorModel>()
                .ForMember(dest => dest.Matricula, m => m.MapFrom(src => src.Matricula.Valor));

            CreateMap<Operador, OperadorData>()
                .ForMember(dest => dest.Matricula, m => m.MapFrom(src => src.Matricula.Valor));

            CreateMap<OperadorData, OperadorRetornoModel>();
        }
    }
}
