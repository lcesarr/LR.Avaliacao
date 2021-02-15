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
    public class MarcaRepository : BaseRepository<MarcaData>, IMarcaRepository
    {
        private const string queryObterPor = @"SELECT Id, Descricao
                                                 FROM Marca 
                                                WHERE Descricao = ISNULL(@descricao, Descricao)";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public MarcaRepository(Lazy<SqlConnection> connectionString) : base(connectionString) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        public async Task<IEnumerable<MarcaData>> ObterPor(string descricao)
        {
            using (IDbConnection conn = base.Conexao().Value)
                return await conn.QueryAsync<MarcaData>(queryObterPor, new
                {
                    login = new DbString { IsAnsi = true, Length = 100, Value = string.IsNullOrWhiteSpace(descricao) ? null : $"%{descricao}%" }
                });
        }
    }
}