using Dapper.FluentMap.Dommel.Mapping;
using LR.Avaliacao.Domain.EntitiesData;

namespace LR.Avaliacao.Infrastructure.DommelDapperMap
{
    public class MarcaDommelDapperMap : DommelEntityMap<MarcaData>
    {
        public MarcaDommelDapperMap()
        {
            ToTable("Marca");
            Map(x => x.Id).ToColumn("id").IsKey();
            Map(x => x.Descricao).ToColumn("Descricao");
        }
    }
}
