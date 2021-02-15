using LR.Avaliacao.Domain.EntitiesData;
using LR.Avaliacao.Domain.Repositories;
using LR.Avaliacao.Infrastructure.Base;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LR.Avaliacao.Tests.Mocks
{
    public class UsuarioRepositoryMock : BaseRepository<UsuarioData>, IUsuarioRepository
    {
        private readonly static Mock<IUsuarioRepository> _mock = new Mock<IUsuarioRepository>();
        public Mock<IUsuarioRepository> Mock { get { return _mock; } }

        public UsuarioRepositoryMock()
        {
            Mock.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Guid id) =>
            {
                return Task.FromResult(UsuarioData().AsQueryable().Where(q => q.Id == id).FirstOrDefault());
            });

            Mock.Setup(x => x.ObterPor(It.IsAny<string>())).Returns((string login) =>
            {
                return Task.FromResult(UsuarioData().AsQueryable().Where(q => (string.IsNullOrWhiteSpace(login) || (!string.IsNullOrWhiteSpace(login) && q.Login.Contains(login)))).AsEnumerable());
            });

            Mock.Setup(x => x.Incluir(It.IsAny<UsuarioData>())).Returns((UsuarioData UsuarioData) =>
            {
                return Task.FromResult(null as object);
            });

            Mock.Setup(x => x.Alterar(It.IsAny<UsuarioData>())).Returns((UsuarioData UsuarioData) =>
            {
                return Task.FromResult(true);
            });

            Mock.Setup(x => x.Excluir(It.IsAny<UsuarioData>())).Returns((UsuarioData UsuarioData) =>
            {
                return Task.FromResult(true);
            });

            Mock.Setup(x => x.Autenticar(It.IsAny<string>(), It.IsAny<string>())).Returns((string login, string senha) =>
            {
                return Task.FromResult(UsuarioData().AsQueryable().FirstOrDefault(q => q.Login == login && q.Senha == senha));
            });
        }

        public override async Task<UsuarioData> ObterPorId(Guid id)
        {
            return await Mock.Object.ObterPorId(id);
        }

        public override async Task<object> Incluir(UsuarioData entity)
        {
            return await Mock.Object.Incluir(entity);
        }

        public override async Task<bool> Alterar(UsuarioData entity)
        {
            return await Mock.Object.Alterar(entity);
        }
        public override async Task<bool> Excluir(UsuarioData entity)
        {
            return await Mock.Object.Excluir(entity);
        }

        public async Task<IEnumerable<UsuarioData>> ObterPor(string login)
        {
            return await Mock.Object.ObterPor(login);
        }

        public async Task<UsuarioData> Autenticar(string login, string senha)
        {
            return await Mock.Object.Autenticar(login, senha);
        }

        private IEnumerable<UsuarioData> UsuarioData()
        {
            return new List<UsuarioData>()
                {
                    new UsuarioData(){
                        Id = Guid.Parse("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b"),
                        Login = "01234567890",
                        Senha = "Passw0rd"
                    },
                    new UsuarioData(){
                        Id = Guid.Parse("4106aa8c-5a3e-4c53-92b1-df78c377dd4e"),
                        Login = "123456",
                        Senha = "Pa$$w0rd"
                    }
                };
        }
    }
}
