using LR.Avaliacao.Domain.EntitiesData;
using LR.Avaliacao.Domain.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LR.Avaliacao.Domain.Repositories
{
    public interface IOperadorRepository : IBaseRepository<OperadorData>
    {
        Task<IEnumerable<OperadorData>> ObterPor(string nome, string matricula);
    }
}
