using Dapper.FluentMap.Dommel.Mapping;
using LR.Avaliacao.Domain.EntitiesData;

namespace LR.Avaliacao.Infrastructure.DommelDapperMap
{
    public class UsuarioDommelDapperMap : DommelEntityMap<UsuarioData>
    {
        public UsuarioDommelDapperMap()
        {
            ToTable("Usuario");
            Map(x => x.Id).ToColumn("id").IsKey();
            Map(x => x.Login).ToColumn("Login");
            Map(x => x.Senha).ToColumn("Senha");
        }
    }
}
