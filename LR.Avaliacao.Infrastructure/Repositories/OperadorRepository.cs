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
    public class OperadorRepository : BaseRepository<OperadorData>, IOperadorRepository
    {
        private const string queryObterPor = @"SELECT Id, Matricula
                                                 FROM Operador 
                                                WHERE Nome LIKE ISNULL(@nome, Nome) 
                                                  AND Matricula  = ISNULL(@matricula, Matricula)";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public OperadorRepository(Lazy<SqlConnection> connectionString) : base(connectionString) { }

        public async Task<IEnumerable<OperadorData>> ObterPor(string nome, string matricula)
        {
            using (IDbConnection conn = base.Conexao().Value)
                return await conn.QueryAsync<OperadorData>(queryObterPor, new
                {
                    nome = new DbString { IsAnsi = true, Length = 100, Value = string.IsNullOrWhiteSpace(nome) ? null : $"%{nome}%" },
                    matricula
                });
        }
    }
}