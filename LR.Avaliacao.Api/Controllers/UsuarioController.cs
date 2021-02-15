using LR.Avaliacao.Api.Controllers.Base;
using LR.Avaliacao.Application.Interfaces;
using LR.Avaliacao.Application.Models.Usuario;
using LR.Avaliacao.Application.Resultado;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LR.Avaliacao.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ApiBaseController
    {
        private readonly IUsuarioApplication _usuarioApplication;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioApplication"></param>
        public UsuarioController(IUsuarioApplication usuarioApplication)
        {
            _usuarioApplication = usuarioApplication;
        }

        /// <summary>
        /// Retorna uma lista de Usuarios conforme parâmetros informados
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<UsuarioRetornoModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> Listar(string login)
        {
            var retorno = await _usuarioApplication.Listar(login);
            return Ok(retorno.Object);
        }

        /// <summary>
        /// Cadastro de novos Usuarioes
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UsuarioModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Incluir(UsuarioModel UsuarioModel)
        {
            var retorno = await _usuarioApplication.Incluir(UsuarioModel);
            if (retorno.Valid)
                return Ok(retorno.Object);
            return BadRequest(retorno.Notifications);
        }

        /// <summary>
        /// Altera dados de Usuarioes já existentes
        /// </summary>
        /// <param name="id">Identificador do registro</param>
        /// <param name="UsuarioModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UsuarioModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Alterar(Guid id, UsuarioModel UsuarioModel)
        {
            var retorno = await _usuarioApplication.Alterar(id, UsuarioModel);
            if (retorno.Valid)
                return Ok(retorno.Object);
            return BadRequest(retorno.Notifications);
        }

        /// <summary>
        /// Altera dados de Usuarioes já existentes
        /// </summary>
        /// <param name="id">Identificador do registro</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UsuarioRetornoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var retorno = await _usuarioApplication.Obter(id);
            return Ok(retorno.Object);
        }

        /// <summary>
        /// Altera dados de Usuarioes já existentes
        /// </summary>
        /// <param name="id">Identificador do registro</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var retorno = await _usuarioApplication.Excluir(id);
            if (retorno.Valid)
                return Ok();
            return BadRequest(retorno.Notifications);
        }

        /// <summary>
        /// Autenticar usuário
        /// </summary>
        /// <param name="usuarioModel">Objeto para autenticação</param>
        /// <returns></returns>
        [HttpPost]
        [Route("autenticar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UsuarioRetornoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Autenticar(UsuarioModel usuarioModel)
        {
            var retorno = await _usuarioApplication.Autenticar(usuarioModel);
            if (retorno.Valid)
                return Ok(retorno.Object);
            return BadRequest(retorno.Notifications);
        }
    }
}