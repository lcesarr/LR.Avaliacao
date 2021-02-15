using LR.Avaliacao.Application.Application;
using LR.Avaliacao.Application.Interfaces;
using LR.Avaliacao.Domain.Repositories;
using LR.Avaliacao.Infrastructure.DommelDapperMap.Registrar;
using LR.Avaliacao.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.SqlClient;

namespace LR.Avaliacao.Ioc.ResolveDependecia
{
    public static class ResolveDependencias
    {
        public static void AddResolverDependencies(this IServiceCollection services)
        {
            services.AddScoped<IClienteApplication, ClienteApplication>();
            services.AddScoped<IClienteRepository, ClienteRepository>();

            RegistrarMapTabelas.Register();

            services.AddTransient(scope =>
            {
                return new Lazy<SqlConnection>(() =>
                {
                    var conexao = new SqlConnection("");
                    conexao.Open();
                    return conexao;
                });
            });
        }
    }
}
