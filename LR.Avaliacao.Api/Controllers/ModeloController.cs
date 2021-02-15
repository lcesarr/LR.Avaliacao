using LR.Avaliacao.Api.Controllers.Base;
using LR.Avaliacao.Application.Interfaces;
using LR.Avaliacao.Application.Models.Modelo;
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
    public class ModeloController : ApiBaseController
    {
        private readonly IModeloApplication _ModeloApplication;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ModeloApplication"></param>
        public ModeloController(IModeloApplication ModeloApplication)
        {
            _ModeloApplication = ModeloApplication;
        }

        /// <summary>
        /// Retorna uma lista de Modelos conforme parâmetros informados
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<ModeloRetornoModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> Listar(string descricao)
        {
            var retorno = await _ModeloApplication.Listar(descricao);
            return Ok(retorno.Object);
        }

        /// <summary>
        /// Cadastro de novos Modeloes
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModeloModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Incluir(ModeloModel ModeloModel)
        {
            var retorno = await _ModeloApplication.Incluir(ModeloModel);
            if (retorno.Valid)
                return Ok(retorno.Object);
            return BadRequest(retorno.Notifications);
        }

        /// <summary>
        /// Altera dados de Modeloes já existentes
        /// </summary>
        /// <param name="id">Identificador do registro</param>
        /// <param name="ModeloModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModeloModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Alterar(Guid id, ModeloModel ModeloModel)
        {
            var retorno = await _ModeloApplication.Alterar(id, ModeloModel);
            if (retorno.Valid)
                return Ok(retorno.Object);
            return BadRequest(retorno.Notifications);
        }

        /// <summary>
        /// Altera dados de Modeloes já existentes
        /// </summary>
        /// <param name="id">Identificador do registro</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ModeloRetornoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var retorno = await _ModeloApplication.Obter(id);
            return Ok(retorno.Object);
        }

        /// <summary>
        /// Altera dados de Modeloes já existentes
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
            var retorno = await _ModeloApplication.Excluir(id);
            if (retorno.Valid)
                return Ok();
            return BadRequest(retorno.Notifications);
        }
    }
}