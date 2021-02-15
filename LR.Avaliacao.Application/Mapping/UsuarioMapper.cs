using AutoMapper;
using LR.Avaliacao.Application.Models.Usuario;
using LR.Avaliacao.Domain.Entities;
using LR.Avaliacao.Domain.EntitiesData;

namespace LR.Avaliacao.Application.Mapping
{
    public class UsuarioMapper : Profile
    {
        public UsuarioMapper()
        {
            CreateMap<UsuarioModel, Usuario>()
                .ConstructUsing(src =>
                    new Usuario(src.Login, src.Senha));

            CreateMap<Usuario, UsuarioModel>();

            CreateMap<Usuario, UsuarioData>();

            CreateMap<UsuarioData, UsuarioRetornoModel>();
        }
    }
}
