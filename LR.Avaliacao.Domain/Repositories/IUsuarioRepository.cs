using LR.Avaliacao.Domain.EntitiesData;
using LR.Avaliacao.Domain.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LR.Avaliacao.Domain.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUsuarioRepository : IBaseRepository<UsuarioData>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<IEnumerable<UsuarioData>> ObterPor(string login);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        Task<UsuarioData> Autenticar(string login, string senha);
    }
}
