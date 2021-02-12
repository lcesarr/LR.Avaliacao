using Flunt.Notifications;
using Flunt.Validations;

namespace LR.Avaliacao.Domain.ValueObjects
{
    public class Cpf : Notifiable
    {
        public Cpf(string cpf)
        {
            Valor = cpf;

            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Valor, nameof(Valor), "Numero do Cpf não pode ser nulo ou branco")
                .HasLen(Valor, 11, nameof(Valor), "Numero do Cpf deve conter 11 caracteres")
                .Matchs(Valor, @"^\d{11}", nameof(Valor), "Numero do Cpf deve conter apenas números"));
        }

        public string Valor { get; private set; }
    }
}
