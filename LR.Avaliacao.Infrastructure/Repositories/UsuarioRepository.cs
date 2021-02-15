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
    public class UsuarioRepository : BaseRepository<UsuarioData>, IUsuarioRepository
    {
        private const string queryObterPor = @"SELECT Id, Login, Senha
                                                 FROM Usuario 
                                                WHERE login = ISNULL(@login, login)";

        private const string queryAutenticar = @"SELECT Id, Login, Senha
                                                 FROM Usuario 
                                                WHERE login = @login
                                                  ABD Senha = @senha";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public UsuarioRepository(Lazy<SqlConnection> connectionString) : base(connectionString) { }

        public async Task<IEnumerable<UsuarioData>> ObterPor(string login)
        {
            using (IDbConnection conn = base.Conexao().Value)
                return await conn.QueryAsync<UsuarioData>(queryObterPor, new
                {
                    login = new DbString { IsAnsi = true, Length = 100, Value = string.IsNullOrWhiteSpace(login) ? null : $"%{login}%" }
                });
        }

        public async Task<UsuarioData> Autenticar(string login, string senha)
        {
            using (IDbConnection conn = base.Conexao().Value)
                return await conn.QueryFirstOrDefaultAsync<UsuarioData>(queryAutenticar, new
                {
                    login = new DbString { IsAnsi = true, Length = 11, Value = login },
                    senha = new DbString { IsAnsi = true, Length = 500, Value = senha }
                });
        }
    }
}