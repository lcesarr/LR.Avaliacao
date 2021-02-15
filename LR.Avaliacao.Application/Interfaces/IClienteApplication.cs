using LR.Avaliacao.Application.Models.Cliente;
using LR.Avaliacao.Application.Resultado;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LR.Avaliacao.Application.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IClienteApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="cpf"></param>
        /// <param name="dataAniversarioInicio"></param>
        /// <param name="dataAniversarioFim"></param>
        /// <returns></returns>
        Task<Retorno<IEnumerable<ClienteRetornoModel>>> Listar(string nome, string cpf, DateTime? dataAniversarioInicio, DateTime? dataAniversarioFim);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Retorno<ClienteRetornoModel>> Obter(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clienteModel"></param>
        /// <returns></returns>
        Task<Retorno<ClienteRetornoModel>> Incluir(ClienteModel clienteModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clienteModel"></param>
        /// <returns></returns>
        Task<Retorno<ClienteRetornoModel>> Alterar(Guid id, ClienteModel clienteModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Retorno> Excluir(Guid id);
    }
}
