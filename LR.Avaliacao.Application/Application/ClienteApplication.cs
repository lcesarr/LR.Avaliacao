using AutoMapper;
using Flunt.Notifications;
using LR.Avaliacao.Application.Interfaces;
using LR.Avaliacao.Application.Models.Cliente;
using LR.Avaliacao.Application.Resultado;
using LR.Avaliacao.Domain.Entities;
using LR.Avaliacao.Domain.EntitiesData;
using LR.Avaliacao.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LR.Avaliacao.Application.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class ClienteApplication : IClienteApplication
    {
        readonly private IMapper _mapper;
        readonly private IClienteRepository _clienteRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="clienteRepository"></param>
        public ClienteApplication(IMapper mapper, IClienteRepository clienteRepository)
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clienteModel"></param>
        /// <returns></returns>
        public async Task<Retorno<ClienteRetornoModel>> Alterar(Guid id, ClienteModel clienteModel)
        {
            var cliente = _mapper.Map<ClienteModel, Cliente>(clienteModel);
            cliente.AlterarId(id);
            if (!cliente.Valid) return Retorno<ClienteRetornoModel>.Error(cliente.Notifications);

            var clienteData = await _clienteRepository.ObterPorId(id);
            if (clienteData != null)
            {
                var clienteAlterar = _mapper.Map<Cliente, ClienteData>(cliente);
                if (await _clienteRepository.Alterar(clienteAlterar))
                    return Retorno<ClienteRetornoModel>.Ok();
                return Retorno<ClienteRetornoModel>.Error(new Notification("Erro", "Falha ao alterar o cliente"));
            }
            else
                return Retorno<ClienteRetornoModel>.Error(new Notification("Erro", "Cliente não encontrado"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Retorno> Excluir(Guid id)
        {
            var clienteData = await _clienteRepository.ObterPorId(id);
            if (clienteData != null)
            {
                if (await _clienteRepository.Excluir(clienteData))
                    return Retorno.Ok();
                return Retorno.Error(new Notification("Erro", "Falha ao excluir o cliente"));
            }
            else
                return Retorno.Error(new Notification("Erro", "Cliente não encontrado"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clienteModel"></param>
        /// <returns></returns>
        public async Task<Retorno<ClienteRetornoModel>> Incluir(ClienteModel clienteModel)
        {
            var cliente = _mapper.Map<ClienteModel, Cliente>(clienteModel);
            if (!cliente.Valid) return Retorno<ClienteRetornoModel>.Error(cliente.Notifications);

            var clienteIncluir = _mapper.Map<Cliente, ClienteData>(cliente);
            return Retorno<ClienteRetornoModel>.Ok(_mapper.Map<ClienteData, ClienteRetornoModel>((ClienteData)(await _clienteRepository.Incluir(clienteIncluir))));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="cpf"></param>
        /// <param name="dataAniversarioInicio"></param>
        /// <param name="dataAniversarioFim"></param>
        /// <returns></returns>
        public async Task<Retorno<IEnumerable<ClienteRetornoModel>>> Listar(string nome, string cpf, DateTime? dataAniversarioInicio, DateTime? dataAniversarioFim)
        {
            return Retorno<IEnumerable<ClienteRetornoModel>>
                .Ok(_mapper.Map<IEnumerable<ClienteData>, IEnumerable<ClienteRetornoModel>>
                (await _clienteRepository.ObterPor(nome, cpf, dataAniversarioInicio, dataAniversarioFim)));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Retorno<ClienteRetornoModel>> Obter(Guid id)
        {
            return Retorno<ClienteRetornoModel>.Ok(_mapper.Map<ClienteData, ClienteRetornoModel>(await _clienteRepository.ObterPorId(id)));
        }
    }
}
