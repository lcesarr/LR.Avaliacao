using LR.Avaliacao.Domain.Entities;
using Xunit;

namespace LR.Avaliacao.Tests.Domain
{
    public class MarcaTest
    {
        [Theory]
        [InlineData("ABS")]
        [InlineData("Teste de Marca")]
        public void MarcaDadosValidos(string descricao)
        {
            var Marca = new Marca(descricao);
            Assert.True(Marca.Valid);
        }

        [Theory]
        [InlineData("KS")]
        [InlineData("Teste de Marca 12345678901 Teste de Marca 12345678901 Teste de Marca 12345678901 Teste de Marca 12345678901")]
        public void MarcaDadosInValidos(string descricao)
        {
            var Marca = new Marca(descricao);
            Assert.False(Marca.Valid);
        }
    }
}
