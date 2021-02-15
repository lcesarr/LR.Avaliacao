using AutoMapper;
using Flunt.Notifications;
using LR.Avaliacao.Application.Interfaces;
using LR.Avaliacao.Application.Models.Modelo;
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
    public class ModeloApplication : IModeloApplication
    {
        readonly private IMapper _mapper;
        readonly private IModeloRepository _modeloRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="modeloRepository"></param>
        public ModeloApplication(IMapper mapper, IModeloRepository modeloRepository)
        {
            _mapper = mapper;
            _modeloRepository = modeloRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modeloModel"></param>
        /// <returns></returns>
        public async Task<Retorno<ModeloRetornoModel>> Alterar(Guid id, ModeloModel modeloModel)
        {
            var modelo = _mapper.Map<ModeloModel, Modelo>(modeloModel);
            modelo.AlterarId(id);
            if (!modelo.Valid) return Retorno<ModeloRetornoModel>.Error(modelo.Notifications);

            var modeloData = await _modeloRepository.ObterPorId(id);
            if (modeloData != null)
            {
                var modeloAlterar = _mapper.Map<Modelo, ModeloData>(modelo);
                if (await _modeloRepository.Alterar(modeloAlterar))
                    return Retorno<ModeloRetornoModel>.Ok();
                return Retorno<ModeloRetornoModel>.Error(new Notification("Erro", "Falha ao alterar o Modelo"));
            }
            else
                return Retorno<ModeloRetornoModel>.Error(new Notification("Erro", "Modelo não encontrado"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Retorno> Excluir(Guid id)
        {
            var modeloData = await _modeloRepository.ObterPorId(id);
            if (modeloData != null)
            {
                if (await _modeloRepository.Excluir(modeloData))
                    return Retorno.Ok();
                return Retorno.Error(new Notification("Erro", "Falha ao excluir o Modelo"));
            }
            else
                return Retorno.Error(new Notification("Erro", "Modelo não encontrado"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ModeloModel"></param>
        /// <returns></returns>
        public async Task<Retorno<ModeloRetornoModel>> Incluir(ModeloModel modeloModel)
        {
            var modelo = _mapper.Map<ModeloModel, Modelo>(modeloModel);
            if (!modelo.Valid) return Retorno<ModeloRetornoModel>.Error(modelo.Notifications);

            var modeloIncluir = _mapper.Map<Modelo, ModeloData>(modelo);
            return Retorno<ModeloRetornoModel>.Ok(_mapper.Map<ModeloData, ModeloRetornoModel>((ModeloData)(await _modeloRepository.Incluir(modeloIncluir))));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descricao"></param>
        /// <param name="cpf"></param>
        /// <param name="dataAniversarioInicio"></param>
        /// <param name="dataAniversarioFim"></param>
        /// <returns></returns>
        public async Task<Retorno<IEnumerable<ModeloRetornoModel>>> Listar(string descricao)
        {
            return Retorno<IEnumerable<ModeloRetornoModel>>
                .Ok(_mapper.Map<IEnumerable<ModeloData>, IEnumerable<ModeloRetornoModel>>
                (await _modeloRepository.ObterPor(descricao)));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Retorno<ModeloRetornoModel>> Obter(Guid id)
        {
            return Retorno<ModeloRetornoModel>.Ok(_mapper.Map<ModeloData, ModeloRetornoModel>(await _modeloRepository.ObterPorId(id)));
        }
    }
}
