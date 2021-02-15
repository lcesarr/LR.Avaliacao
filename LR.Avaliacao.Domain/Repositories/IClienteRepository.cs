using LR.Avaliacao.Domain.EntitiesData;
using LR.Avaliacao.Domain.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LR.Avaliacao.Domain.Repositories
{
    public interface IClienteRepository : IBaseRepository<ClienteData>
    {
        Task<IEnumerable<ClienteData>> ObterPor(string nome, string cpf, DateTime? dataAniversarioInicio, DateTime? dataAniversarioFim);
    }
}
