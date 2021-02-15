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
    public class MarcaRepositoryMock : BaseRepository<MarcaData>, IMarcaRepository
    {
        private readonly static Mock<IMarcaRepository> _mock = new Mock<IMarcaRepository>();
        public Mock<IMarcaRepository> Mock { get { return _mock; } }

        public MarcaRepositoryMock()
        {
            Mock.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Guid id) =>
            {
                return Task.FromResult(MarcaData().AsQueryable().Where(q => q.Id == id).FirstOrDefault());
            });

            Mock.Setup(x => x.ObterPor(It.IsAny<string>())).Returns((string descricao) =>
            {
                return Task.FromResult(MarcaData().AsQueryable().Where(q => (string.IsNullOrWhiteSpace(descricao) || (!string.IsNullOrWhiteSpace(descricao) && q.Descricao.Contains(descricao)))).AsEnumerable());
            });

            Mock.Setup(x => x.Incluir(It.IsAny<MarcaData>())).Returns((MarcaData MarcaData) =>
            {
                return Task.FromResult(null as object);
            });

            Mock.Setup(x => x.Alterar(It.IsAny<MarcaData>())).Returns((MarcaData MarcaData) =>
            {
                return Task.FromResult(true);
            });

            Mock.Setup(x => x.Excluir(It.IsAny<MarcaData>())).Returns((MarcaData MarcaData) =>
            {
                return Task.FromResult(true);
            });
        }

        public override async Task<MarcaData> ObterPorId(Guid id)
        {
            return await Mock.Object.ObterPorId(id);
        }

        public override async Task<object> Incluir(MarcaData entity)
        {
            return await Mock.Object.Incluir(entity);
        }

        public override async Task<bool> Alterar(MarcaData entity)
        {
            return await Mock.Object.Alterar(entity);
        }
        public override async Task<bool> Excluir(MarcaData entity)
        {
            return await Mock.Object.Excluir(entity);
        }

        public async Task<IEnumerable<MarcaData>> ObterPor(string login)
        {
            return await Mock.Object.ObterPor(login);
        }

        private IEnumerable<MarcaData> MarcaData()
        {
            return new List<MarcaData>()
                {
                    new MarcaData(){
                        Id = Guid.Parse("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b"),
                        Descricao = "ABS"
                    },
                    new MarcaData(){
                        Id = Guid.Parse("4106aa8c-5a3e-4c53-92b1-df78c377dd4e"),
                        Descricao = "Teste de Marca"
                    }
                };
        }
    }
}
