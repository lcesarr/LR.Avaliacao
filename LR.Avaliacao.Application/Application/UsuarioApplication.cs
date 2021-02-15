using AutoMapper;
using Flunt.Notifications;
using LR.Avaliacao.Application.Interfaces;
using LR.Avaliacao.Application.Models.Usuario;
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
    public class UsuarioApplication : IUsuarioApplication
    {
        readonly private IMapper _mapper;
        readonly private IUsuarioRepository _UsuarioRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="UsuarioRepository"></param>
        public UsuarioApplication(IMapper mapper, IUsuarioRepository UsuarioRepository)
        {
            _mapper = mapper;
            _UsuarioRepository = UsuarioRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UsuarioModel"></param>
        /// <returns></returns>
        public async Task<Retorno<UsuarioRetornoModel>> Alterar(Guid id, UsuarioModel UsuarioModel)
        {
            var Usuario = _mapper.Map<UsuarioModel, Usuario>(UsuarioModel);
            Usuario.AlterarId(id);
            if (!Usuario.Valid) return Retorno<UsuarioRetornoModel>.Error(Usuario.Notifications);

            var UsuarioData = await _UsuarioRepository.ObterPorId(id);
            if (UsuarioData != null)
            {
                var UsuarioAlterar = _mapper.Map<Usuario, UsuarioData>(Usuario);
                if (await _UsuarioRepository.Alterar(UsuarioAlterar))
                    return Retorno<UsuarioRetornoModel>.Ok();
                return Retorno<UsuarioRetornoModel>.Error(new Notification("Erro", "Falha ao alterar o Usuario"));
            }
            else
                return Retorno<UsuarioRetornoModel>.Error(new Notification("Erro", "Usuario não encontrado"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioModel"></param>
        /// <returns></returns>
        public async Task<Retorno<UsuarioRetornoModel>> Autenticar(UsuarioModel usuarioModel)
        {
            var Usuario = _mapper.Map<UsuarioModel, Usuario>(usuarioModel);
            if (!Usuario.Valid) return Retorno<UsuarioRetornoModel>.Error(Usuario.Notifications);

            var UsuarioData = await _UsuarioRepository.Autenticar(usuarioModel.Login, usuarioModel.Senha);
            if (UsuarioData != null)
                return Retorno<UsuarioRetornoModel>.Ok(_mapper.Map<UsuarioData, UsuarioRetornoModel>(UsuarioData));
            else
                return Retorno<UsuarioRetornoModel>.Error(new Notification("Erro", "Usuario ou senha inválidos"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Retorno> Excluir(Guid id)
        {
            var UsuarioData = await _UsuarioRepository.ObterPorId(id);
            if (UsuarioData != null)
            {
                if (await _UsuarioRepository.Excluir(UsuarioData))
                    return Retorno.Ok();
                return Retorno.Error(new Notification("Erro", "Falha ao excluir o Usuario"));
            }
            else
                return Retorno.Error(new Notification("Erro", "Usuario não encontrado"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UsuarioModel"></param>
        /// <returns></returns>
        public async Task<Retorno<UsuarioRetornoModel>> Incluir(UsuarioModel UsuarioModel)
        {
            var Usuario = _mapper.Map<UsuarioModel, Usuario>(UsuarioModel);
            if (!Usuario.Valid) return Retorno<UsuarioRetornoModel>.Error(Usuario.Notifications);

            var UsuarioIncluir = _mapper.Map<Usuario, UsuarioData>(Usuario);
            return Retorno<UsuarioRetornoModel>.Ok(_mapper.Map<UsuarioData, UsuarioRetornoModel>((UsuarioData)(await _UsuarioRepository.Incluir(UsuarioIncluir))));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="cpf"></param>
        /// <param name="dataAniversarioInicio"></param>
        /// <param name="dataAniversarioFim"></param>
        /// <returns></returns>
        public async Task<Retorno<IEnumerable<UsuarioRetornoModel>>> Listar(string login)
        {
            return Retorno<IEnumerable<UsuarioRetornoModel>>
                .Ok(_mapper.Map<IEnumerable<UsuarioData>, IEnumerable<UsuarioRetornoModel>>
                (await _UsuarioRepository.ObterPor(login)));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Retorno<UsuarioRetornoModel>> Obter(Guid id)
        {
            return Retorno<UsuarioRetornoModel>.Ok(_mapper.Map<UsuarioData, UsuarioRetornoModel>(await _UsuarioRepository.ObterPorId(id)));
        }
    }
}
