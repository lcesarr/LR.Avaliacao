using AutoMapper;
using LR.Avaliacao.Application.Models.Modelo;
using LR.Avaliacao.Domain.Entities;
using LR.Avaliacao.Domain.EntitiesData;

namespace LR.Avaliacao.Application.Mapping
{
    public class ModeloMapper : Profile
    {
        public ModeloMapper()
        {
            CreateMap<ModeloModel, Modelo>()
                .ConstructUsing(src =>
                    new Modelo(src.Descricao));

            CreateMap<Modelo, ModeloModel>();

            CreateMap<Modelo, ModeloData>();

            CreateMap<ModeloData, ModeloRetornoModel>();
        }
    }
}
