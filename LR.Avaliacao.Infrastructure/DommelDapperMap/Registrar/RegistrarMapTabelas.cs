using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using System.Diagnostics.CodeAnalysis;

namespace LR.Avaliacao.Infrastructure.DommelDapperMap.Registrar
{
    [ExcludeFromCodeCoverage]
    public static class RegistrarMapTabelas
    {
        public static void Register()
        {
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new ClienteDommelDapperMap());

                config.ForDommel();
            });
        }
    }
}
