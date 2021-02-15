using LR.Avaliacao.Api.Controllers;
using LR.Avaliacao.Application.Application;
using LR.Avaliacao.Application.Models.Usuario;
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
    public class UsuarioControllerTest
    {
        private readonly MapperFixture _mapperFixture;
        private readonly IUsuarioRepository _UsuarioRepository;
        public UsuarioControllerTest(MapperFixture mapperFixture)
        {
            _mapperFixture = mapperFixture;
            _UsuarioRepository = new UsuarioRepositoryMock();
        }
        private UsuarioController CriarCotacaoController()
        {
            var cotacaoApplication = new UsuarioApplication(_mapperFixture.Mapper, _UsuarioRepository);
            return new UsuarioController(cotacaoApplication);
        }

        [Theory]
        [InlineData("123456")]
        [InlineData("01234567890")]
        public async Task ListarUsuarioSucessoTestAsync(string login)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Listar(login);
            Assert.IsType<OkObjectResult>(result);
            Assert.True(((IEnumerable<UsuarioRetornoModel>)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value)).Count() > 0);
        }

        [Theory]
        [InlineData("123459")]
        [InlineData("84297165058")]
        public async Task ListarUsuarioSucessoNaoEncontradoTestAsync(string login)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Listar(login);
            Assert.IsType<OkObjectResult>(result);
            Assert.True(((IEnumerable<UsuarioRetornoModel>)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value)).Count() == 0);
        }

        [Theory]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b")]
        [InlineData("4106aa8c-5a3e-4c53-92b1-df78c377dd4e")]
        public async Task ObterUsuarioPorIdSucessoTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.ObterPorId(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<OkObjectResult>(result);
            Assert.True((UsuarioRetornoModel)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value) != null);
        }

        [Theory]
        [InlineData("ad57d3ac-f91e-4b43-a830-9c4806979694")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public async Task ObterUsuarioPorIdSucessoNaoEncontradoTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.ObterPorId(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<OkObjectResult>(result);
            Assert.True((UsuarioRetornoModel)(((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value) == null);
        }

        [Theory]
        [InlineData("123456", "Pa$$w0rd")]
        [InlineData("01234567890", "Passw0rd")]
        public async Task IncluirUsuarioSucessoTestAsync(string login, string senha)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Incluir(new UsuarioModel
            {
                Login = login,
                Senha = senha
            });
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("1234567", "Pa$$w0rd")]
        [InlineData("123456", "password")]
        [InlineData("012345678901", "Pa$$w0rd")]
        [InlineData("12345678901", "Pa$$w0rd")]
        [InlineData("AA1234", "Pa$$w0rd")]
        [InlineData("", "")]
        public async Task IncluirUsuarioBadRequestTestAsync(string login, string senha)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Incluir(new UsuarioModel
            {
                Login = login,
                Senha = senha
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b", "01234567890", "Test&123")]
        public async Task AlterarUsuarioSucessoTestAsync(string id, string login, string senha)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Alterar(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id), new UsuarioModel
            {
                Login = login,
                Senha = senha
            });
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData("ad57d3ac-f91e-4b43-a830-9c4806979694", "01234567890", "Test&123")]
        [InlineData("00000000-0000-0000-0000-000000000000", "84297165058", "Pa$$w0rd")]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b", "01234567890", "password")]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b", "123456", "password")]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b", "12AA23", "Pa$$w0rd")]
        [InlineData("", "", "")]
        public async Task AlterarUsuarioBadRequestTestAsync(string id, string login, string senha)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Alterar(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id), new UsuarioModel
            {
                Login = login,
                Senha = senha
            });
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData("71d58e64-04ca-4b1b-b055-bcc9e4f7e97b")]
        [InlineData("4106aa8c-5a3e-4c53-92b1-df78c377dd4e")]
        public async Task ExcluirUsuarioSucessoTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Excluir(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<OkResult>(result);
        }

        [Theory]
        [InlineData("ad57d3ac-f91e-4b43-a830-9c4806979694")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public async Task ExcluirUsuarioBadRequestTestAsync(string id)
        {
            var controller = CriarCotacaoController();
            var result = await controller.Excluir(string.IsNullOrWhiteSpace(id) ? Guid.Empty : Guid.Parse(id));
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
