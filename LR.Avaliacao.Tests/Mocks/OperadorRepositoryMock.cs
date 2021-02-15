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
    public class OperadorRepositoryMock : BaseRepository<OperadorData>, IOperadorRepository
    {
        private readonly static Mock<IOperadorRepository> _mock = new Mock<IOperadorRepository>();
        public Mock<IOperadorRepository> Mock { get { return _mock; } }

        public OperadorRepositoryMock()
        {
            Mock.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Guid id) =>
            {
                return Task.FromResult(OperadorData().AsQueryable().Where(q => q.Id == id).FirstOrDefault());
            });

            Mock.Setup(x => x.ObterPor(It.IsAny<string>(), It.IsAny<string>())).Returns((string nome, string matricula) =>
            {
                return Task.FromResult(OperadorData().AsQueryable().Where(q => (string.IsNullOrWhiteSpace(nome) || (!string.IsNullOrWhiteSpace(nome) && q.Nome.Contains(nome))) &&
                                                                              (string.IsNullOrWhiteSpace(matricula) || (!string.IsNullOrWhiteSpace(matricula) && q.Matricula == matricula))).AsEnumerable());
            });

            Mock.Setup(x => x.Incluir(It.IsAny<OperadorData>())).Returns((OperadorData OperadorData) =>
            {
                return Task.FromResult(null as object);
            });

            Mock.Setup(x => x.Alterar(It.IsAny<OperadorData>())).Returns((OperadorData OperadorData) =>
            {
                return Task.FromResult(true);
            });

            Mock.Setup(x => x.Excluir(It.IsAny<OperadorData>())).Returns((OperadorData OperadorData) =>
            {
                return Task.FromResult(true);
            });
        }

        public override async Task<OperadorData> ObterPorId(Guid id)
        {
            return await Mock.Object.ObterPorId(id);
        }

        public override async Task<object> Incluir(OperadorData entity)
        {
            return await Mock.Object.Incluir(entity);
        }

        public override async Task<bool> Alterar(OperadorData entity)
        {
            return await Mock.Object.Alterar(entity);
        }
        public override async Task<bool> Excluir(OperadorData entity)
        {
            return await Mock.Object.Excluir(entity);
        }

        public async Task<IEnumerable<OperadorData>> ObterPor(string nome, string matricula)
        {
            return await Mock.Object.ObterPor(nome, matricula);
        }

        private IEnumerable<OperadorData> OperadorData()
        {
            return new List<OperadorData>()
                {
                    new OperadorData(){
                        Id = Guid.Parse("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b"),
                        Nome = "João da SIlva",
                        Matricula = "123456"
                    },
                    new OperadorData(){
                        Id = Guid.Parse("4106aa8c-5a3e-4c53-92b1-df78c377dd4e"),
                        Nome = "Marina da Silva",
                        Matricula = "123457"
                    },
                new OperadorData(){
                        Id = Guid.Parse("fb0af9b6-0dce-4f9f-be1e-4e27e616629b"),
                        Nome = "Antonia da SIlva",
                        Matricula = "123458"
                    },
                };
        }
    }
}
