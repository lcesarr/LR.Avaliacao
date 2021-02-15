using Dapper.FluentMap.Dommel.Mapping;
using LR.Avaliacao.Domain.EntitiesData;

namespace LR.Avaliacao.Infrastructure.DommelDapperMap
{
    public class ModeloDommelDapperMap : DommelEntityMap<ModeloData>
    {
        public ModeloDommelDapperMap()
        {
            ToTable("Modelo");
            Map(x => x.Id).ToColumn("id").IsKey();
            Map(x => x.Descricao).ToColumn("Descricao");
        }
    }
}
