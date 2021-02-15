using LR.Avaliacao.Application.Models.Operador;
using LR.Avaliacao.Application.Resultado;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LR.Avaliacao.Application.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOperadorApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="matricula"></param>
        /// <returns></returns>
        Task<Retorno<IEnumerable<OperadorRetornoModel>>> Listar(string nome, string matricula);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Retorno<OperadorRetornoModel>> Obter(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OperadorModel"></param>
        /// <returns></returns>
        Task<Retorno<OperadorRetornoModel>> Incluir(OperadorModel OperadorModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="OperadorModel"></param>
        /// <returns></returns>
        Task<Retorno<OperadorRetornoModel>> Alterar(Guid id, OperadorModel OperadorModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Retorno> Excluir(Guid id);
    }
}
