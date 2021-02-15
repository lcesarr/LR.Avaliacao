using AutoMapper;
using Flunt.Notifications;
using LR.Avaliacao.Application.Interfaces;
using LR.Avaliacao.Application.Models.Marca;
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
    public class MarcaApplication : IMarcaApplication
    {
        readonly private IMapper _mapper;
        readonly private IMarcaRepository _marcaRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="marcaRepository"></param>
        public MarcaApplication(IMapper mapper, IMarcaRepository marcaRepository)
        {
            _mapper = mapper;
            _marcaRepository = marcaRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="marcaModel"></param>
        /// <returns></returns>
        public async Task<Retorno<MarcaRetornoModel>> Alterar(Guid id, MarcaModel marcaModel)
        {
            var marca = _mapper.Map<MarcaModel, Marca>(marcaModel);
            marca.AlterarId(id);
            if (!marca.Valid) return Retorno<MarcaRetornoModel>.Error(marca.Notifications);

            var marcaData = await _marcaRepository.ObterPorId(id);
            if (marcaData != null)
            {
                var marcaAlterar = _mapper.Map<Marca, MarcaData>(marca);
                if (await _marcaRepository.Alterar(marcaAlterar))
                    return Retorno<MarcaRetornoModel>.Ok();
                return Retorno<MarcaRetornoModel>.Error(new Notification("Erro", "Falha ao alterar o Marca"));
            }
            else
                return Retorno<MarcaRetornoModel>.Error(new Notification("Erro", "Marca não encontrado"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Retorno> Excluir(Guid id)
        {
            var marcaData = await _marcaRepository.ObterPorId(id);
            if (marcaData != null)
            {
                if (await _marcaRepository.Excluir(marcaData))
                    return Retorno.Ok();
                return Retorno.Error(new Notification("Erro", "Falha ao excluir o Marca"));
            }
            else
                return Retorno.Error(new Notification("Erro", "Marca não encontrado"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="marcaModel"></param>
        /// <returns></returns>
        public async Task<Retorno<MarcaRetornoModel>> Incluir(MarcaModel marcaModel)
        {
            var marca = _mapper.Map<MarcaModel, Marca>(marcaModel);
            if (!marca.Valid) return Retorno<MarcaRetornoModel>.Error(marca.Notifications);

            var marcaIncluir = _mapper.Map<Marca, MarcaData>(marca);
            return Retorno<MarcaRetornoModel>.Ok(_mapper.Map<MarcaData, MarcaRetornoModel>((MarcaData)(await _marcaRepository.Incluir(marcaIncluir))));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descricao"></param>
        /// <param name="cpf"></param>
        /// <param name="dataAniversarioInicio"></param>
        /// <param name="dataAniversarioFim"></param>
        /// <returns></returns>
        public async Task<Retorno<IEnumerable<MarcaRetornoModel>>> Listar(string descricao)
        {
            return Retorno<IEnumerable<MarcaRetornoModel>>
                .Ok(_mapper.Map<IEnumerable<MarcaData>, IEnumerable<MarcaRetornoModel>>
                (await _marcaRepository.ObterPor(descricao)));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Retorno<MarcaRetornoModel>> Obter(Guid id)
        {
            return Retorno<MarcaRetornoModel>.Ok(_mapper.Map<MarcaData, MarcaRetornoModel>(await _marcaRepository.ObterPorId(id)));
        }
    }
}
