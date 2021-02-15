using LR.Avaliacao.Api.Controllers;
using LR.Avaliacao.Application.Application;
using LR.Avaliacao.Application.Models.Marca;
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
    public class MarcaControllerTest
    {
        private readonly MapperFixture _mapperFixture;
        private readonly IMarcaRepository _marcaRepository;
        public MarcaControllerTest(MapperFixture mapperFixture)
        {
            _mapperFixture = mapperFixture;
            _marcaRepository = new MarcaRepositoryMock();
        }
        private MarcaController CriarCotacaoController()
        {
            var cotacaoApplication = new MarcaApplication(_mapperFixture.Mapper, _marcaRepository);
            return new MarcaController(cotacaoApplication);
        }

        [Theory]
        [InlineData("ABS")]
        [InlineData("Teste")]
        public async Task ListarMarcaSucessoTestAsync(string descricao)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Listar(descricao);
            Assert.IsType<OkObjectResult>(result);
            Assert.True(((IEnumerable<MarcaRetornoModel>)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value)).Count() > 0);
        }

        [Theory]
        [InlineData("AXS")]
        [InlineData("Nova marca")]
        public async Task ListarMarcaSucessoNaoEncontradoTestAsync(string descricao)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Listar(descricao);
            Assert.IsType<OkObjectResult>(result);
            Assert.True(((IEnumerable<MarcaRetornoModel>)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value)).Count() == 0);
        }

        [Theory]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b")]
        [InlineData("4106aa8c-5a3e-4c53-92b1-df78c377dd4e")]
        public async Task ObterMarcaPorIdSucessoTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.ObterPorId(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<OkObjectResult>(result);
            Assert.True((MarcaRetornoModel)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value) != null);
        }

        [Theory]
        [InlineData("ad57d3ac-f91e-4b43-a830-9c4806979694")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public async Task ObterMarcaPorIdSucessoNaoEncontradoTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.ObterPorId(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<OkObjectResult>(result);
            Assert.True((MarcaRetornoModel)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value) == null);
        }

        [Theory]
        [InlineData("Nova Marca")]
        public async Task IncluirMarcaSucessoTestAsync(string descricao)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Incluir(new MarcaModel
            {
                Descricao = descricao
            });
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("N2")]
        public async Task IncluirMarcaBadRequestTestAsync(string descricao)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Incluir(new MarcaModel
            {
                Descricao = descricao
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b", "Marca Alterada")]
        public async Task AlterarMarcaSucessoTestAsync(string id, string descricao)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Alterar(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id), new MarcaModel
            {
                Descricao = descricao
            });
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("ad57d3ac-f91e-4b43-a830-9c4806979694", "Marca alterada")]
        [InlineData("00000000-0000-0000-0000-000000000000", "Alterada")]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b", "NS")]
        [InlineData("", "")]
        public async Task AlterarMarcaBadRequestTestAsync(string id, string descricao)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Alterar(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id), new MarcaModel
            {
                Descricao = descricao
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b")]
        [InlineData("4106aa8c-5a3e-4c53-92b1-df78c377dd4e")]
        public async Task ExcluirMarcaSucessoTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Excluir(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<OkResult>(result);
        }

        [Theory]
        [InlineData("ad57d3ac-f91e-4b43-a830-9c4806979694")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public async Task ExcluirMarcaBadRequestTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Excluir(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
