using Dapper;
using LR.Avaliacao.Domain.EntitiesData;
using LR.Avaliacao.Domain.Repositories;
using LR.Avaliacao.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace LR.Avaliacao.Infrastructure.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ClienteRepository : BaseRepository<ClienteData>, IClienteRepository
    {
        private const string queryObterPor = @"SELECT Id, Nome, Cpf, Aniversario 
                                                 FROM Cliente 
                                                WHERE Nome = ISNULL(@nome, Nome) 
                                                  AND Cpf  = ISNULL(@cpf, Cpf)
                                                  AND Aniversario between ISNULL(@dataAniversarioInicio, Aniversario) AND ISNULL(@dataAniversarioFim, Aniversario)";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public ClienteRepository(Lazy<SqlConnection> connectionString) : base(connectionString) { }

        public async Task<IEnumerable<ClienteData>> ObterPor(string nome, string cpf, DateTime? dataAniversarioInicio, DateTime? dataAniversarioFim)
        {
            using (IDbConnection conn = base.Conexao().Value)
                return await conn.QueryAsync<ClienteData>(queryObterPor, new
                {
                    nome = new DbString { IsAnsi = true, Length = 100, Value = string.IsNullOrWhiteSpace(nome) ? null : $"%{nome}%" },
                    cpf = new DbString { IsAnsi = true, Length = 11, Value = cpf },
                    dataAniversarioInicio,
                    dataAniversarioFim
                });
        }
    }
}