using AutoMapper;
using Flunt.Notifications;
using LR.Avaliacao.Application.Interfaces;
using LR.Avaliacao.Application.Models.Operador;
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
    public class OperadorApplication : IOperadorApplication
    {
        readonly private IMapper _mapper;
        readonly private IOperadorRepository _OperadorRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="OperadorRepository"></param>
        public OperadorApplication(IMapper mapper, IOperadorRepository OperadorRepository)
        {
            _mapper = mapper;
            _OperadorRepository = OperadorRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="OperadorModel"></param>
        /// <returns></returns>
        public async Task<Retorno<OperadorRetornoModel>> Alterar(Guid id, OperadorModel OperadorModel)
        {
            var Operador = _mapper.Map<OperadorModel, Operador>(OperadorModel);
            Operador.AlterarId(id);
            if (!Operador.Valid) return Retorno<OperadorRetornoModel>.Error(Operador.Notifications);

            var OperadorData = await _OperadorRepository.ObterPorId(id);
            if (OperadorData != null)
            {
                var OperadorAlterar = _mapper.Map<Operador, OperadorData>(Operador);
                if (await _OperadorRepository.Alterar(OperadorAlterar))
                    return Retorno<OperadorRetornoModel>.Ok();
                return Retorno<OperadorRetornoModel>.Error(new Notification("Erro", "Falha ao alterar o Operador"));
            }
            else
                return Retorno<OperadorRetornoModel>.Error(new Notification("Erro", "Operador não encontrado"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Retorno> Excluir(Guid id)
        {
            var OperadorData = await _OperadorRepository.ObterPorId(id);
            if (OperadorData != null)
            {
                if (await _OperadorRepository.Excluir(OperadorData))
                    return Retorno.Ok();
                return Retorno.Error(new Notification("Erro", "Falha ao excluir o Operador"));
            }
            else
                return Retorno.Error(new Notification("Erro", "Operador não encontrado"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OperadorModel"></param>
        /// <returns></returns>
        public async Task<Retorno<OperadorRetornoModel>> Incluir(OperadorModel OperadorModel)
        {
            var Operador = _mapper.Map<OperadorModel, Operador>(OperadorModel);
            if (!Operador.Valid) return Retorno<OperadorRetornoModel>.Error(Operador.Notifications);

            var OperadorIncluir = _mapper.Map<Operador, OperadorData>(Operador);
            return Retorno<OperadorRetornoModel>.Ok(_mapper.Map<OperadorData, OperadorRetornoModel>((OperadorData)(await _OperadorRepository.Incluir(OperadorIncluir))));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="cpf"></param>
        /// <param name="dataAniversarioInicio"></param>
        /// <param name="dataAniversarioFim"></param>
        /// <returns></returns>
        public async Task<Retorno<IEnumerable<OperadorRetornoModel>>> Listar(string nome, string matricula)
        {
            return Retorno<IEnumerable<OperadorRetornoModel>>
                .Ok(_mapper.Map<IEnumerable<OperadorData>, IEnumerable<OperadorRetornoModel>>
                (await _OperadorRepository.ObterPor(nome, matricula)));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Retorno<OperadorRetornoModel>> Obter(Guid id)
        {
            return Retorno<OperadorRetornoModel>.Ok(_mapper.Map<OperadorData, OperadorRetornoModel>(await _OperadorRepository.ObterPorId(id)));
        }
    }
}
