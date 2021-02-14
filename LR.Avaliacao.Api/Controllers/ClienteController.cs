using LR.Avaliacao.Application.Interfaces;
using LR.Avaliacao.Application.Models.Cliente;
using LR.Avaliacao.Application.Resultado;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LR.Avaliacao.Api.Controllers
{
    [ApiController]
    [Route("[cliente]")]
    public class ClienteController : Controller
    {
        private readonly IClienteApplication _clienteApplication;
        public ClienteController(IClienteApplication clienteApplication)
        {
            _clienteApplication = clienteApplication;
        }

        /// <summary>
        /// Retorna uma lista de clientes conforme parâmetros informados
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="cpf"></param>
        /// <param name="dataAniversarioInicio"></param>
        /// <param name="dataAniversarioFim"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<ClienteRetornoModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<IActionResult> Listar(string nome, string cpf, DateTime? dataAniversarioInicio, DateTime? dataAniversarioFim)
        {
            var retorno = await _clienteApplication.Listar(nome, cpf, dataAniversarioInicio, dataAniversarioFim);
            if (retorno.Valid)
                return Ok(retorno.Object);
            return BadRequest(retorno.Notifications);
        }

        /// <summary>
        /// Cadastro de novos clientes
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Incluir(ClienteModel clienteModel)
        {
            var retorno = await _clienteApplication.Incluir(clienteModel);
            if (retorno.Valid)
                return Ok(retorno.Object);
            return BadRequest(retorno.Notifications);
        }

        /// <summary>
        /// Altera dados de clientes já existentes
        /// </summary>
        /// <param name="id">Identificador do registro</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClienteModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Alterar(Guid id, ClienteModel clienteModel)
        {
            var retorno = await _clienteApplication.Alterar(id, clienteModel);
            if (retorno.Valid)
                return Ok(retorno.Object);
            return BadRequest(retorno.Notifications);
        }

        /// <summary>
        /// Altera dados de clientes já existentes
        /// </summary>
        /// <param name="id">Identificador do registro</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClienteRetornoModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Erro), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(Retorno), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var retorno = await _clienteApplication.Obter(id);
            if (retorno.Valid)
                return Ok(retorno.Object);
            return BadRequest(retorno.Notifications);
        }

        /// <summary>
        /// Altera dados de clientes já existentes
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
            var retorno = await _clienteApplication.Excluir(id);
            if (retorno.Valid)
                return Ok();
            return BadRequest(retorno.Notifications);
        }
    }
}