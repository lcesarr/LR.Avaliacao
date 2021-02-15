using Flunt.Notifications;
using Flunt.Validations;

namespace LR.Avaliacao.Domain.ValueObjects
{
    public class Matricula : Notifiable
    {
        public Matricula(string matricula)
        {
            Valor = matricula;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Valor, nameof(Valor), "Numero da matricula não pode ser nulo ou branco")
                .HasLen(Valor, 6, nameof(Valor), "Numero do matricula deve conter 6 caracteres")
                .Matchs(Valor, @"^\d{6}", nameof(Valor), "Numero do matricula deve conter apenas números")
                .IsFalse(Valor == "000000", nameof(Valor), "Numero do matricula deve ser diferentes de '000000'"));
        }

        public string Valor { get; private set; }
    }
}
