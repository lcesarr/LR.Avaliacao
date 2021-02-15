using Dapper.FluentMap.Dommel.Mapping;
using LR.Avaliacao.Domain.EntitiesData;

namespace LR.Avaliacao.Infrastructure.DommelDapperMap
{
    public class OperadorDommelDapperMap : DommelEntityMap<OperadorData>
    {
        public OperadorDommelDapperMap()
        {
            ToTable("Operador");
            Map(x => x.Id).ToColumn("id").IsKey();
            Map(x => x.Nome).ToColumn("Nome");
            Map(x => x.Matricula).ToColumn("Matricula");
        }
    }
}
