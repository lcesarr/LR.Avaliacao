using Dommel;
using LR.Avaliacao.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace LR.Avaliacao.Infrastructure.Base
{
    [ExcludeFromCodeCoverage]
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly Lazy<SqlConnection> _connectionString;

        public BaseRepository(Lazy<SqlConnection> connectionString)
        {
            _connectionString = connectionString;
        }

        public BaseRepository()
        { }

        public virtual Lazy<SqlConnection> Conexao()
        {
            if (_connectionString.IsValueCreated
                && _connectionString.Value != null
                && !string.IsNullOrWhiteSpace(_connectionString.Value.ConnectionString)) return _connectionString;

            return new Lazy<SqlConnection>(() =>
            {
                var conexao = new SqlConnection("");
                conexao.Open();
                return conexao;
            });
        }

        public virtual async Task<bool> Alterar(TEntity entity)
        {
            using (IDbConnection conn = Conexao().Value)
                return await conn.UpdateAsync(entity);
        }

        public virtual async Task<bool> Excluir(TEntity entity)
        {
            using (IDbConnection conn = Conexao().Value)
                return await conn.DeleteAsync(entity);
        }

        public virtual async Task<object> Incluir(TEntity entity)
        {
            using (IDbConnection conn = Conexao().Value)
                return await conn.InsertAsync(entity);
        }

        public virtual async Task<IEnumerable<TEntity>> ListarTodos()
        {
            using (IDbConnection conn = Conexao().Value)
                return await conn.GetAllAsync<TEntity>();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            using (IDbConnection conn = Conexao().Value)
                return await conn.GetAsync<TEntity>(id);
        }
    }
}