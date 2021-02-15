using LR.Avaliacao.Domain.EntitiesData;
using LR.Avaliacao.Domain.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LR.Avaliacao.Domain.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMarcaRepository : IBaseRepository<MarcaData>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        Task<IEnumerable<MarcaData>> ObterPor(string descricao);
    }
}
