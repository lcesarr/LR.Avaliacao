using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LR.Avaliacao.Domain.Repositories.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Lazy<SqlConnection> Conexao();
        Task<IEnumerable<TEntity>> ListarTodos();

        Task<TEntity> ObterPorId(Guid id);

        Task<object> Incluir(TEntity entity);

        Task<bool> Alterar(TEntity entity);

        Task<bool> Excluir(TEntity entity);
    }
}
