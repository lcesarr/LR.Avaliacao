using LR.Avaliacao.Domain.Entities;
using Xunit;

namespace LR.Avaliacao.Tests.Domain
{
    public class ModeloTest
    {
        [Theory]
        [InlineData("KSD")]
        [InlineData("Fiat")]
        public void ModeloDadosValidos(string descricao)
        {
            var Modelo = new Modelo(descricao);
            Assert.True(Modelo.Valid);
        }

        [Theory]
        [InlineData("KS")]
        public void ModeloDadosInValidos(string descricao)
        {
            var Modelo = new Modelo(descricao);
            Assert.False(Modelo.Valid);
        }
    }
}
