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
    public class ModeloRepositoryMock : BaseRepository<ModeloData>, IModeloRepository
    {
        private readonly static Mock<IModeloRepository> _mock = new Mock<IModeloRepository>();
        public Mock<IModeloRepository> Mock { get { return _mock; } }

        public ModeloRepositoryMock()
        {
            Mock.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Guid id) =>
            {
                return Task.FromResult(ModeloData().AsQueryable().Where(q => q.Id == id).FirstOrDefault());
            });

            Mock.Setup(x => x.ObterPor(It.IsAny<string>())).Returns((string descricao) =>
            {
                return Task.FromResult(ModeloData().AsQueryable().Where(q => (string.IsNullOrWhiteSpace(descricao) || (!string.IsNullOrWhiteSpace(descricao) && q.Descricao.Contains(descricao)))).AsEnumerable());
            });

            Mock.Setup(x => x.Incluir(It.IsAny<ModeloData>())).Returns((ModeloData ModeloData) =>
            {
                return Task.FromResult(null as object);
            });

            Mock.Setup(x => x.Alterar(It.IsAny<ModeloData>())).Returns((ModeloData ModeloData) =>
            {
                return Task.FromResult(true);
            });

            Mock.Setup(x => x.Excluir(It.IsAny<ModeloData>())).Returns((ModeloData ModeloData) =>
            {
                return Task.FromResult(true);
            });
        }

        public override async Task<ModeloData> ObterPorId(Guid id)
        {
            return await Mock.Object.ObterPorId(id);
        }

        public override async Task<object> Incluir(ModeloData entity)
        {
            return await Mock.Object.Incluir(entity);
        }

        public override async Task<bool> Alterar(ModeloData entity)
        {
            return await Mock.Object.Alterar(entity);
        }
        public override async Task<bool> Excluir(ModeloData entity)
        {
            return await Mock.Object.Excluir(entity);
        }

        public async Task<IEnumerable<ModeloData>> ObterPor(string login)
        {
            return await Mock.Object.ObterPor(login);
        }

        private IEnumerable<ModeloData> ModeloData()
        {
            return new List<ModeloData>()
                {
                    new ModeloData(){
                        Id = Guid.Parse("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b"),
                        Descricao = "KDS"
                    },
                    new ModeloData(){
                        Id = Guid.Parse("4106aa8c-5a3e-4c53-92b1-df78c377dd4e"),
                        Descricao = "Teste de Modelo"
                    }
                };
        }
    }
}
