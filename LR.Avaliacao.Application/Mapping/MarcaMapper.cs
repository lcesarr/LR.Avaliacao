using AutoMapper;
using LR.Avaliacao.Application.Models.Marca;
using LR.Avaliacao.Domain.Entities;
using LR.Avaliacao.Domain.EntitiesData;

namespace LR.Avaliacao.Application.Mapping
{
    public class MarcaMapper : Profile
    {
        public MarcaMapper()
        {
            CreateMap<MarcaModel, Marca>()
                .ConstructUsing(src =>
                    new Marca(src.Descricao));

            CreateMap<Marca, MarcaModel>();

            CreateMap<Marca, MarcaData>();

            CreateMap<MarcaData, MarcaRetornoModel>();
        }
    }
}
