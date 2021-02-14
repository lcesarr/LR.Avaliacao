using LR.Avaliacao.Api.Controllers;
using LR.Avaliacao.Application.Application;
using LR.Avaliacao.Application.Models.Cliente;
using LR.Avaliacao.Tests.Mapper.Fixture;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LR.Avaliacao.Tests.Controllers
{
    //[Collection("Mapper")]
    public class ClienteControllerTest
    {
        private readonly MapperFixture _mapperFixture;

        public ClienteControllerTest(MapperFixture mapperFixture)
        {
            _mapperFixture = mapperFixture;
        }
        private ClienteController CriarCotacaoController()
        {
            var cotacaoApplication = new ClienteApplication(_mapperFixture.Mapper);
            return new ClienteController(cotacaoApplication);
        }

        [Theory]
        [InlineData("Leandro Ruela", "01234567890", "1985-01-01", "2002-01-01")]
        [InlineData("Leandro Ruela", "", "", "")]
        [InlineData("", "01234567890", "", "")]
        [InlineData("", "", "1985-01-01", "2002-01-01")]
        public async Task ListarClienteSucessoTestAsync(string nome, string cpf, string dataAniversarioInicio, string dataAniversarioFim)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Listar(nome, cpf, !string.IsNullOrWhiteSpace(dataAniversarioInicio) ? DateTime.Parse(dataAniversarioInicio) : DateTime.MinValue, !string.IsNullOrWhiteSpace(dataAniversarioFim) ? DateTime.Parse(dataAniversarioFim) : DateTime.MinValue);
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("1234")]
        [InlineData("r432")]
        [InlineData("543w")]
        public async Task ObterClientePorIdSucessoTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.ObterPorId(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("Leandro Ruela", "01234567890", "1985-01-01")]
        [InlineData("Leandro Ruela", "", "")]
        [InlineData("", "01234567890", "")]
        [InlineData("", "", "1985-01-01")]
        public async Task IncluirClienteSucessoTestAsync(string nome, string cpf, string dataAniversario)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Incluir(new ClienteModel
            {
                Nome = nome,
                Cpf = cpf,
                Aniversario = !string.IsNullOrWhiteSpace(dataAniversario) ? DateTime.Parse(dataAniversario) : DateTime.MinValue
            }); ;
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("", "Leandro Ruela", "01234567890", "1985-01-01")]
        [InlineData("", "Leandro Ruela", "", "")]
        [InlineData("", "", "01234567890", "")]
        [InlineData("", "", "", "1985-01-01")]
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
        [InlineData("1234")]
        [InlineData("r432")]
        [InlineData("543w")]
        public async Task ExcluirClienteSucessoTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Excluir(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<OkResult>(result);
        }
    }
}
