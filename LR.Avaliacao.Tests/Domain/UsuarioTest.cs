using LR.Avaliacao.Domain.Entities;
using Xunit;

namespace LR.Avaliacao.Tests.Domain
{
    public class UsuarioTest
    {
        [Theory]
        [InlineData("01234567890", "Passw0rd")]
        [InlineData("123456", "Pa$$word")]
        public void UsuarioDadosValidos(string login, string senha)
        {
            var Usuario = new Usuario(login, senha);
            Assert.True(Usuario.Valid);
        }

        [Theory]
        [InlineData("12345", "Passw0rd")]
        [InlineData("123456789012", "Passw0rd")]
        [InlineData("123456", "12345678")]
        [InlineData("01234567890", "123456")]
        [InlineData("123456", "")]
        [InlineData("", "Passw0rd")]
        [InlineData("", "")]
        public void UsuarioDadosInValidos(string login, string senha)
        {
            var Usuario = new Usuario(login, senha);
            Assert.False(Usuario.Valid);
        }
    }
}
