using AutoMapper;
using LR.Avaliacao.Application.Models.Cliente;
using LR.Avaliacao.Domain.Entities;

namespace LR.Avaliacao.Application.Mapping
{
    public class ClienteMapper : Profile
    {
        public ClienteMapper()
        {
            CreateMap<ClienteModel, Cliente>();
            //CreateMap<Cliente, CaracteristicaDbModel>();
            //CreateMap<CaracteristicaDbModel, CaracteristicaModel>();
            CreateMap<Cliente, ClienteModel>();

            //CreateMap<InserirCaracteristicaModel, Caracteristica>()
            //    .ForMember(caracteristica => caracteristica.Descricao, m => m.Ignore())
            //    .ConstructUsing(inserirCaracteristicaModel =>
            //        new Caracteristica(inserirCaracteristicaModel.Descricao));

            //CreateMap<AtivarInativarCaracteristicaModel, Caracteristica>()
            //    .ForMember(caracteristica => caracteristica.Id, m => m.Ignore())
            //    .ForMember(caracteristica => caracteristica.UsuarioAlteracao, m => m.Ignore())
            //    .ConstructUsing(inserirCaracteristicaModel =>
            //        new Caracteristica(inserirCaracteristicaModel.Id, inserirCaracteristicaModel.UsuarioAlteracao));
        }
    }
}
