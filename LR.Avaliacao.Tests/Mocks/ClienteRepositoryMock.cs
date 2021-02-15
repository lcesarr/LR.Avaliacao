using LR.Avaliacao.Domain.EntitiesData;
using LR.Avaliacao.Domain.Repositories;
using LR.Avaliacao.Infrastructure.Base;
using LR.Avaliacao.Util.Validacoes;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LR.Avaliacao.Tests.Mocks
{
    public class ClienteRepositoryMock : BaseRepository<ClienteData>, IClienteRepository
    {
        private readonly static Mock<IClienteRepository> _mock = new Mock<IClienteRepository>();
        public Mock<IClienteRepository> Mock { get { return _mock; } }

        public ClienteRepositoryMock()
        {
            Mock.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Guid id) =>
            {
                return Task.FromResult(ClienteData().AsQueryable().Where(q => q.Id == id).FirstOrDefault());
            });

            Mock.Setup(x => x.ObterPor(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns((string nome, string cpf, DateTime? dataAniversarioInicio, DateTime? dataAniversarioFim) =>
            {
                return Task.FromResult(ClienteData().AsQueryable().Where(q => (string.IsNullOrWhiteSpace(nome) || (!string.IsNullOrWhiteSpace(nome) && q.Nome.Contains(nome))) &&
                                                                              (string.IsNullOrWhiteSpace(cpf) || (!string.IsNullOrWhiteSpace(cpf) && q.Cpf == cpf)) &&
                                                                              (!Dados.ValidarData(dataAniversarioInicio) || (Dados.ValidarData(dataAniversarioInicio) && q.Aniversario >= dataAniversarioInicio)) &&
                                                                              (!Dados.ValidarData(dataAniversarioFim) || (Dados.ValidarData(dataAniversarioFim) && q.Aniversario <= dataAniversarioFim))).AsEnumerable());
            });

            Mock.Setup(x => x.Incluir(It.IsAny<ClienteData>())).Returns((ClienteData clienteData) =>
            {
                return Task.FromResult(null as object);
            });

            Mock.Setup(x => x.Alterar(It.IsAny<ClienteData>())).Returns((ClienteData clienteData) =>
            {
                return Task.FromResult(true);
            });

            Mock.Setup(x => x.Excluir(It.IsAny<ClienteData>())).Returns((ClienteData clienteData) =>
            {
                return Task.FromResult(true);
            });
        }

        public override async Task<ClienteData> ObterPorId(Guid id)
        {
            return await Mock.Object.ObterPorId(id);
        }

        public override async Task<object> Incluir(ClienteData entity)
        {
            return await Mock.Object.Incluir(entity);
        }

        public override async Task<bool> Alterar(ClienteData entity)
        {
            return await Mock.Object.Alterar(entity);
        }
        public override async Task<bool> Excluir(ClienteData entity)
        {
            return await Mock.Object.Excluir(entity);
        }

        public async Task<IEnumerable<ClienteData>> ObterPor(string nome, string cpf, DateTime? dataAniversarioInicio, DateTime? dataAniversarioFim)
        {
            return await Mock.Object.ObterPor(nome, cpf, dataAniversarioInicio, dataAniversarioFim);
        }

        private IEnumerable<ClienteData> ClienteData()
        {
            return new List<ClienteData>()
                {
                    new ClienteData(){
                        Id = Guid.Parse("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b"),
                        Nome = "João da SIlva",
                        Aniversario = new DateTime(1985, 10, 27),
                        Cpf = "01234567890"
                    },
                    new ClienteData(){
                        Id = Guid.Parse("4106aa8c-5a3e-4c53-92b1-df78c377dd4e"),
                        Nome = "Marina da Silva",
                        Aniversario = new DateTime(1995, 01, 01),
                        Cpf = "84297165058"
                    },
                new ClienteData(){
                        Id = Guid.Parse("fb0af9b6-0dce-4f9f-be1e-4e27e616629b"),
                        Nome = "Antonia da SIlva",
                        Aniversario = DateTime.Now.AddYears(-18),
                        Cpf = "56281283090"
                    },
                };
        }
    }
}
