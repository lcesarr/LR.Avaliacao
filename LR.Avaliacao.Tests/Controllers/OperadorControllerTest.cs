using LR.Avaliacao.Api.Controllers;
using LR.Avaliacao.Application.Application;
using LR.Avaliacao.Application.Models.Operador;
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
    public class OperadorControllerTest
    {
        private readonly MapperFixture _mapperFixture;
        private readonly IOperadorRepository _OperadorRepository;
        public OperadorControllerTest(MapperFixture mapperFixture)
        {
            _mapperFixture = mapperFixture;
            _OperadorRepository = new OperadorRepositoryMock();
        }
        private OperadorController CriarCotacaoController()
        {
            var cotacaoApplication = new OperadorApplication(_mapperFixture.Mapper, _OperadorRepository);
            return new OperadorController(cotacaoApplication);
        }

        [Theory]
        [InlineData("Marina da Silva", "123457")]
        [InlineData("João da SIlva", "")]
        [InlineData("", "123458")]
        public async Task ListarOperadorSucessoTestAsync(string nome, string matricula)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Listar(nome, matricula);
            Assert.IsType<OkObjectResult>(result);
            Assert.True(((IEnumerable<OperadorRetornoModel>)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value)).Count() > 0);
        }

        [Theory]
        [InlineData("Joarez Antonio", "123456")]
        [InlineData("Marcelo Silva", "")]
        [InlineData("", "123459")]
        public async Task ListarOperadorSucessoNaoEncontradoTestAsync(string nome, string matricula)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Listar(nome, matricula);
            Assert.IsType<OkObjectResult>(result);
            Assert.True(((IEnumerable<OperadorRetornoModel>)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value)).Count() == 0);
        }

        [Theory]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b")]
        [InlineData("4106aa8c-5a3e-4c53-92b1-df78c377dd4e")]
        [InlineData("fb0af9b6-0dce-4f9f-be1e-4e27e616629b")]
        public async Task ObterOperadorPorIdSucessoTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.ObterPorId(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<OkObjectResult>(result);
            Assert.True((OperadorRetornoModel)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value) != null);
        }

        [Theory]
        [InlineData("ad57d3ac-f91e-4b43-a830-9c4806979694")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public async Task ObterOperadorPorIdSucessoNaoEncontradoTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.ObterPorId(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<OkObjectResult>(result);
            Assert.True((OperadorRetornoModel)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value) == null);
        }

        [Theory]
        [InlineData("Leandro Ruela", "123456")]
        [InlineData("Leandro Cesar Ruela", "123456")]
        public async Task IncluirOperadorSucessoTestAsync(string nome, string matricula)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Incluir(new OperadorModel
            {
                Nome = nome,
                Matricula = matricula
            });
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("Marcio Mendes  ", "123456")]
        [InlineData("Leandro Cesar Ruela", "1234")]
        [InlineData("Leandro Cesar Ruela", "1234567")]
        public async Task IncluirOperadorBadRequestTestAsync(string nome, string matricula)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Incluir(new OperadorModel
            {
                Nome = nome,
                Matricula = matricula
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b", "Leandro Ruela", "123457")]
        public async Task AlterarOperadorSucessoTestAsync(string id, string nome, string matricula)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Alterar(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id), new OperadorModel
            {
                Nome = nome,
                Matricula = matricula
            });
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("ad57d3ac-f91e-4b43-a830-9c4806979694", "Leandro Ruela", "01234567890")]
        [InlineData("00000000-0000-0000-0000-000000000000", "Leandro Ruela", "01234567890")]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b", "Leandro Ruela", "1234579")]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b", "Leandro  Ruela", "123459")]
        public async Task AlterarOperadorBadRequestTestAsync(string id, string nome, string matricula)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Alterar(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id), new OperadorModel
            {
                Nome = nome,
                Matricula = matricula
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b")]
        [InlineData("4106aa8c-5a3e-4c53-92b1-df78c377dd4e")]
        [InlineData("fb0af9b6-0dce-4f9f-be1e-4e27e616629b")]
        public async Task ExcluirOperadorSucessoTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Excluir(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<OkResult>(result);
        }

        [Theory]
        [InlineData("ad57d3ac-f91e-4b43-a830-9c4806979694")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public async Task ExcluirOperadorBadRequestTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Excluir(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
