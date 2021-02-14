using AutoMapper;
using LR.Avaliacao.Application.Interfaces;
using LR.Avaliacao.Application.Models.Cliente;
using LR.Avaliacao.Application.Resultado;
using LR.Avaliacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LR.Avaliacao.Application.Application
{
    public class ClienteApplication : IClienteApplication
    {
        readonly private IMapper _mapper;
        public ClienteApplication(IMapper mapper)
        {
            _mapper = mapper;
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
            if (cliente.Valid) return Retorno<ClienteRetornoModel>.Ok(new ClienteRetornoModel());

            return Retorno<ClienteRetornoModel>.Error(cliente.Notifications);
        }

        public Task<Retorno> Excluir(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clienteModel"></param>
        /// <returns></returns>
        public async Task<Retorno<ClienteRetornoModel>> Incluir(ClienteModel clienteModel)
        {
            var cliente = _mapper.Map<ClienteModel, Cliente>(clienteModel);
            if (cliente.Valid) return Retorno<ClienteRetornoModel>.Ok(new ClienteRetornoModel());

            return Retorno<ClienteRetornoModel>.Error(cliente.Notifications);
        }

        public Task<Retorno<IEnumerable<ClienteRetornoModel>>> Listar(string nome, string cpf, DateTime? dataAniversarioInicio, DateTime? dataAniversarioFim)
        {
            throw new NotImplementedException();
        }

        public Task<Retorno<ClienteRetornoModel>> Obter(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
