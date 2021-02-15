using System;

namespace LR.Avaliacao.Util.Validacoes
{
    public static class Dados
    {
        public static bool ValidarGuid(Guid? @guid)
        {
            if (guid.HasValue && guid.Value.Equals(Guid.Empty)) return false;
            if (!guid.HasValue) return false;
            return true;
        }

        public static bool ValidarIntMaiorZero(int? @int)
        {
            if (@int.HasValue && @int.Value <= 0) return false;
            if (!@int.HasValue) return false;
            return true;
        }

        public static bool ValidarData(DateTime? @dateTime)
        {
            if (@dateTime.HasValue && @dateTime.Value.Equals(DateTime.MinValue)) return false;
            if (!@dateTime.HasValue) return false;
            return true;
        }

        public static bool ValidarIdadeMinima(DateTime aniversario, int idadeMinima)
        {
            if (DateTime.Now.Date.Subtract(aniversario.Date).Ticks <= 0) return false;
            return (new DateTime(DateTime.Now.Date.Subtract(aniversario.Date).Ticks).Year - 1) >= idadeMinima;
        }
    }
}
