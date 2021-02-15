using LR.Avaliacao.Api.Controllers;
using LR.Avaliacao.Application.Application;
using LR.Avaliacao.Application.Models.Cliente;
using LR.Avaliacao.Domain.Repositories;
using LR.Avaliacao.Tests.Mapper.Fixture;
using LR.Avaliacao.Tests.Mocks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LR.Avaliacao.Tests.Controllers
{
    [Collection("Mapper")]
    public class ClienteControllerTest
    {
        private readonly MapperFixture _mapperFixture;
        private readonly IClienteRepository _clienteRepository;
        public ClienteControllerTest(MapperFixture mapperFixture)
        {
            _mapperFixture = mapperFixture;
            _clienteRepository = new ClienteRepositoryMock();
        }
        private ClienteController CriarCotacaoController()
        {
            var cotacaoApplication = new ClienteApplication(_mapperFixture.Mapper, _clienteRepository);
            return new ClienteController(cotacaoApplication);
        }

        [Theory]
        [InlineData("Marina da Silva", "84297165058", "1995-01-01", "2002-01-01")]
        [InlineData("João da SIlva", "", "", "")]
        [InlineData("", "56281283090", "", "")]
        [InlineData("", "", "1985-01-01", "2010-01-01")]
        [InlineData("", "", "", "")]
        public async Task ListarClienteSucessoTestAsync(string nome, string cpf, string dataAniversarioInicio, string dataAniversarioFim)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Listar(nome, cpf, !string.IsNullOrWhiteSpace(dataAniversarioInicio) ? DateTime.Parse(dataAniversarioInicio) : DateTime.MinValue, !string.IsNullOrWhiteSpace(dataAniversarioFim) ? DateTime.Parse(dataAniversarioFim) : DateTime.MinValue);
            Assert.IsType<OkObjectResult>(result);
            Assert.True(((IEnumerable<ClienteRetornoModel>)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value)).Count() > 0);
        }

        [Theory]
        [InlineData("Joarez Antonio", "84297165058", "1995-01-01", "2002-01-01")]
        [InlineData("Marcelo Silva", "", "", "")]
        [InlineData("", "56893581009", "", "")]
        [InlineData("", "", "1986-01-01", "1994-01-01")]
        public async Task ListarClienteSucessoNaoEncontradoTestAsync(string nome, string cpf, string dataAniversarioInicio, string dataAniversarioFim)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Listar(nome, cpf, !string.IsNullOrWhiteSpace(dataAniversarioInicio) ? DateTime.Parse(dataAniversarioInicio) : DateTime.MinValue, !string.IsNullOrWhiteSpace(dataAniversarioFim) ? DateTime.Parse(dataAniversarioFim) : DateTime.MinValue);
            Assert.IsType<OkObjectResult>(result);
            Assert.True(((IEnumerable<ClienteRetornoModel>)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value)).Count() == 0);
        }

        [Theory]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b")]
        [InlineData("4106aa8c-5a3e-4c53-92b1-df78c377dd4e")]
        [InlineData("fb0af9b6-0dce-4f9f-be1e-4e27e616629b")]
        public async Task ObterClientePorIdSucessoTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.ObterPorId(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<OkObjectResult>(result);
            Assert.True((ClienteRetornoModel)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value) != null);
        }

        [Theory]
        [InlineData("ad57d3ac-f91e-4b43-a830-9c4806979694")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public async Task ObterClientePorIdSucessoNaoEncontradoTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.ObterPorId(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<OkObjectResult>(result);
            Assert.True((ClienteRetornoModel)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value) == null);
        }

        [Theory]
        [InlineData("Leandro Ruela", "01234567890", "1985-01-01")]
        [InlineData("Leandro Cesar Ruela", "01234567890", "1985-10-27")]
        public async Task IncluirClienteSucessoTestAsync(string nome, string cpf, string dataAniversario)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Incluir(new ClienteModel
            {
                Nome = nome,
                Cpf = cpf,
                Aniversario = !string.IsNullOrWhiteSpace(dataAniversario) ? DateTime.Parse(dataAniversario) : DateTime.MinValue
            });
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("Marcio Mendes  ", "01234567890", "1985-01-01")]
        [InlineData("Leandro Cesar Ruela", "12345678901", "1985-10-27")]
        [InlineData("", "", "")]
        public async Task IncluirClienteBadRequestTestAsync(string nome, string cpf, string dataAniversario)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Incluir(new ClienteModel
            {
                Nome = nome,
                Cpf = cpf,
                Aniversario = !string.IsNullOrWhiteSpace(dataAniversario) ? DateTime.Parse(dataAniversario) : DateTime.MinValue
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b", "Leandro Ruela", "01234567890", "1985-01-01")]
        public async Task AlterarClienteSucessoTestAsync(string id, string nome, string cpf, string dataAniversario)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Alterar(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id), new ClienteModel
            {
                Nome = nome,
                Cpf = cpf,
                Aniversario = !string.IsNullOrWhiteSpace(dataAniversario) ? DateTime.Parse(dataAniversario) : DateTime.MinValue
            });
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("ad57d3ac-f91e-4b43-a830-9c4806979694", "Leandro Ruela", "01234567890", "1985-01-01")]
        [InlineData("00000000-0000-0000-0000-000000000000", "Leandro Ruela", "01234567890", "1985-01-01")]
        public async Task AlterarClienteBadRequestTestAsync(string id, string nome, string cpf, string dataAniversario)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Alterar(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id), new ClienteModel
            {
                Nome = nome,
                Cpf = cpf,
                Aniversario = !string.IsNullOrWhiteSpace(dataAniversario) ? DateTime.Parse(dataAniversario) : DateTime.MinValue
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b")]
        [InlineData("4106aa8c-5a3e-4c53-92b1-df78c377dd4e")]
        [InlineData("fb0af9b6-0dce-4f9f-be1e-4e27e616629b")]
        public async Task ExcluirClienteSucessoTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Excluir(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<OkResult>(result);
        }

        [Theory]
        [InlineData("ad57d3ac-f91e-4b43-a830-9c4806979694")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public async Task ExcluirClienteBadRequestTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Excluir(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
