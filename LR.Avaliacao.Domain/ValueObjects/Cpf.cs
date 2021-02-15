﻿using Flunt.Notifications;
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
                .Matchs(Valor, @"^\d{11}", nameof(Valor), "Numero do Cpf deve conter apenas números")
                .IsTrue(IsCPF(), nameof(Valor), "Cpf inválido"));
        }

        private bool IsCPF()
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            // Caso coloque todos os numeros iguais
            switch (Valor)
            {
                case "11111111111":
                case "00000000000":
                case "22222222222":
                case "33333333333":
                case "44444444444":
                case "55555555555":
                case "66666666666":
                case "77777777777":
                case "88888888888":
                case "99999999999":
                    return false;
            }

            Valor = Valor.Trim();
            Valor = Valor.Replace(".", "").Replace("-", "");
            if (Valor.Length != 11)
                return false;
            tempCpf = Valor.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();
            return Valor.EndsWith(digito);
        }

        public string Valor { get; private set; }
    }
}
