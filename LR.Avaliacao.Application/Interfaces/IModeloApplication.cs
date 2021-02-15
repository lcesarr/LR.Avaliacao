using LR.Avaliacao.Application.Models.Modelo;
using LR.Avaliacao.Application.Resultado;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LR.Avaliacao.Application.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModeloApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        Task<Retorno<IEnumerable<ModeloRetornoModel>>> Listar(string descricao);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Retorno<ModeloRetornoModel>> Obter(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ModeloModel"></param>
        /// <returns></returns>
        Task<Retorno<ModeloRetornoModel>> Incluir(ModeloModel ModeloModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ModeloModel"></param>
        /// <returns></returns>
        Task<Retorno<ModeloRetornoModel>> Alterar(Guid id, ModeloModel ModeloModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Retorno> Excluir(Guid id);
    }
}
