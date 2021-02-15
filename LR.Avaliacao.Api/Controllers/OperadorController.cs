using LR.Avaliacao.Api.Controllers.Base;
using LR.Avaliacao.Application.Interfaces;
using LR.Avaliacao.Application.Models.Operador;
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
    public class OperadorController : ApiBaseController
    {
        private readonly IOperadorApplication _operadorApplication;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operadorApplication"></param>
        public OperadorController(IOperadorApplication operadorApplication)
        {
            _operadorApplication = operadorApplication;
        }

        /// <summary>
        /// Retorna uma lista de Operadors conforme parâmetros informados
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="matricula"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<OperadorRetornoModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> Listar(string nome, string matricula)
        {
            var retorno = await _operadorApplication.Listar(nome, matricula);
            return Ok(retorno.Object);
        }

        /// <summary>
        /// Cadastro de novos Operadores
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OperadorModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Incluir(OperadorModel OperadorModel)
        {
            var retorno = await _operadorApplication.Incluir(OperadorModel);
            if (retorno.Valid)
                return Ok(retorno.Object);
            return BadRequest(retorno.Notifications);
        }

        /// <summary>
        /// Altera dados de operadores já existentes
        /// </summary>
        /// <param name="id">Identificador do registro</param>
        /// <param name="OperadorModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OperadorModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Alterar(Guid id, OperadorModel OperadorModel)
        {
            var retorno = await _operadorApplication.Alterar(id, OperadorModel);
            if (retorno.Valid)
                return Ok(retorno.Object);
            return BadRequest(retorno.Notifications);
        }

        /// <summary>
        /// Altera dados de operadores já existentes
        /// </summary>
        /// <param name="id">Identificador do registro</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(OperadorRetornoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var retorno = await _operadorApplication.Obter(id);
            return Ok(retorno.Object);
        }

        /// <summary>
        /// Altera dados de operadores já existentes
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
            var retorno = await _operadorApplication.Excluir(id);
            if (retorno.Valid)
                return Ok();
            return BadRequest(retorno.Notifications);
        }
    }
}