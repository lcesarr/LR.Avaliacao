using LR.Avaliacao.Api.Controllers.Base;
using LR.Avaliacao.Application.Interfaces;
using LR.Avaliacao.Application.Models.Marca;
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
    public class MarcaController : ApiBaseController
    {
        private readonly IMarcaApplication _marcaApplication;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="marcaApplication"></param>
        public MarcaController(IMarcaApplication marcaApplication)
        {
            _marcaApplication = marcaApplication;
        }

        /// <summary>
        /// Retorna uma lista de Marcas conforme parâmetros informados
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<MarcaRetornoModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> Listar(string descricao)
        {
            var retorno = await _marcaApplication.Listar(descricao);
            return Ok(retorno.Object);
        }

        /// <summary>
        /// Cadastro de novos Marcaes
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MarcaModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Incluir(MarcaModel MarcaModel)
        {
            var retorno = await _marcaApplication.Incluir(MarcaModel);
            if (retorno.Valid)
                return Ok(retorno.Object);
            return BadRequest(retorno.Notifications);
        }

        /// <summary>
        /// Altera dados de Marcaes já existentes
        /// </summary>
        /// <param name="id">Identificador do registro</param>
        /// <param name="MarcaModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MarcaModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Alterar(Guid id, MarcaModel MarcaModel)
        {
            var retorno = await _marcaApplication.Alterar(id, MarcaModel);
            if (retorno.Valid)
                return Ok(retorno.Object);
            return BadRequest(retorno.Notifications);
        }

        /// <summary>
        /// Altera dados de Marcaes já existentes
        /// </summary>
        /// <param name="id">Identificador do registro</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MarcaRetornoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var retorno = await _marcaApplication.Obter(id);
            return Ok(retorno.Object);
        }

        /// <summary>
        /// Altera dados de Marcaes já existentes
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
            var retorno = await _marcaApplication.Excluir(id);
            if (retorno.Valid)
                return Ok();
            return BadRequest(retorno.Notifications);
        }
    }
}