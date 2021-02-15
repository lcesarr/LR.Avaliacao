using LR.Avaliacao.Api.Controllers;
using LR.Avaliacao.Application.Application;
using LR.Avaliacao.Application.Models.Modelo;
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
    public class ModeloControllerTest
    {
        private readonly MapperFixture _mapperFixture;
        private readonly IModeloRepository _modeloRepository;
        public ModeloControllerTest(MapperFixture mapperFixture)
        {
            _mapperFixture = mapperFixture;
            _modeloRepository = new ModeloRepositoryMock();
        }
        private ModeloController CriarCotacaoController()
        {
            var cotacaoApplication = new ModeloApplication(_mapperFixture.Mapper, _modeloRepository);
            return new ModeloController(cotacaoApplication);
        }

        [Theory]
        [InlineData("KDS")]
        [InlineData("Teste")]
        public async Task ListarModeloSucessoTestAsync(string descricao)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Listar(descricao);
            Assert.IsType<OkObjectResult>(result);
            Assert.True(((IEnumerable<ModeloRetornoModel>)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value)).Count() > 0);
        }

        [Theory]
        [InlineData("AXS")]
        [InlineData("Novo Modelo")]
        public async Task ListarModeloSucessoNaoEncontradoTestAsync(string descricao)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Listar(descricao);
            Assert.IsType<OkObjectResult>(result);
            Assert.True(((IEnumerable<ModeloRetornoModel>)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value)).Count() == 0);
        }

        [Theory]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b")]
        [InlineData("4106aa8c-5a3e-4c53-92b1-df78c377dd4e")]
        public async Task ObterModeloPorIdSucessoTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.ObterPorId(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<OkObjectResult>(result);
            Assert.True((ModeloRetornoModel)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value) != null);
        }

        [Theory]
        [InlineData("ad57d3ac-f91e-4b43-a830-9c4806979694")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public async Task ObterModeloPorIdSucessoNaoEncontradoTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.ObterPorId(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<OkObjectResult>(result);
            Assert.True((ModeloRetornoModel)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value) == null);
        }

        [Theory]
        [InlineData("Novo Modelo")]
        public async Task IncluirModeloSucessoTestAsync(string descricao)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Incluir(new ModeloModel
            {
                Descricao = descricao
            });
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("N5")]
        public async Task IncluirModeloBadRequestTestAsync(string descricao)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Incluir(new ModeloModel
            {
                Descricao = descricao
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b", "Modelo Alterada")]
        public async Task AlterarModeloSucessoTestAsync(string id, string descricao)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Alterar(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id), new ModeloModel
            {
                Descricao = descricao
            });
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("ad57d3ac-f91e-4b43-a830-9c4806979694", "Modelo alterada")]
        [InlineData("00000000-0000-0000-0000-000000000000", "Alterada")]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b", "NU")]
        [InlineData("", "")]
        public async Task AlterarModeloBadRequestTestAsync(string id, string descricao)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Alterar(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id), new ModeloModel
            {
                Descricao = descricao
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b")]
        [InlineData("4106aa8c-5a3e-4c53-92b1-df78c377dd4e")]
        public async Task ExcluirModeloSucessoTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Excluir(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<OkResult>(result);
        }

        [Theory]
        [InlineData("ad57d3ac-f91e-4b43-a830-9c4806979694")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public async Task ExcluirModeloBadRequestTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Excluir(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
