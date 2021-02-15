using Dapper.FluentMap.Dommel.Mapping;
using LR.Avaliacao.Domain.EntitiesData;

namespace LR.Avaliacao.Infrastructure.DommelDapperMap
{
    public class ClienteDommelDapperMap : DommelEntityMap<ClienteData>
    {
        public ClienteDommelDapperMap()
        {
            ToTable("Cliente");
            Map(x => x.Id).ToColumn("id").IsKey();
            Map(x => x.Nome).ToColumn("Nome");
            Map(x => x.Cpf).ToColumn("Cpf");
            Map(x => x.Aniversario).ToColumn("Aniversario");
        }
    }
}
