using LR.Avaliacao.Application.Models.Usuario;
using LR.Avaliacao.Application.Resultado;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LR.Avaliacao.Application.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUsuarioApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<Retorno<IEnumerable<UsuarioRetornoModel>>> Listar(string login);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Retorno<UsuarioRetornoModel>> Obter(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UsuarioModel"></param>
        /// <returns></returns>
        Task<Retorno<UsuarioRetornoModel>> Incluir(UsuarioModel UsuarioModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UsuarioModel"></param>
        /// <returns></returns>
        Task<Retorno<UsuarioRetornoModel>> Alterar(Guid id, UsuarioModel UsuarioModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Retorno> Excluir(Guid id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UsuarioModel"></param>
        /// <returns></returns>
        Task<Retorno<UsuarioRetornoModel>> Autenticar(UsuarioModel UsuarioModel);
    }
}
