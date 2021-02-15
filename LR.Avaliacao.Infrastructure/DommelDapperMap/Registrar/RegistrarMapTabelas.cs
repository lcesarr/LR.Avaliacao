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
                config.AddMap(new OperadorDommelDapperMap());
                config.AddMap(new UsuarioDommelDapperMap());
                config.AddMap(new MarcaDommelDapperMap());
                config.AddMap(new ModeloDommelDapperMap());

                config.ForDommel();
            });
        }
    }
}
