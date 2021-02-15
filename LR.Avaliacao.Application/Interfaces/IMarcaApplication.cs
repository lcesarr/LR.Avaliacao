using LR.Avaliacao.Application.Models.Marca;
using LR.Avaliacao.Application.Resultado;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LR.Avaliacao.Application.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMarcaApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        Task<Retorno<IEnumerable<MarcaRetornoModel>>> Listar(string descricao);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Retorno<MarcaRetornoModel>> Obter(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MarcaModel"></param>
        /// <returns></returns>
        Task<Retorno<MarcaRetornoModel>> Incluir(MarcaModel MarcaModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="MarcaModel"></param>
        /// <returns></returns>
        Task<Retorno<MarcaRetornoModel>> Alterar(Guid id, MarcaModel MarcaModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Retorno> Excluir(Guid id);
    }
}
