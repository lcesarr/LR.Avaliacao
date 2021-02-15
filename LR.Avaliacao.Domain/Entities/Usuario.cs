using Flunt.Validations;
using LR.Avaliacao.Domain.Core;
using LR.Avaliacao.Domain.Enum;
using LR.Avaliacao.Domain.ValueObjects;
using LR.Avaliacao.Util.AggregateRoot;
using System;

namespace LR.Avaliacao.Domain.Entities
{
    public class Usuario : IdEntity, IAggregateRoot
    {
        public Usuario(string login, string senha)
        {
            Login = login;
            Senha = senha;
            TipoUsuario = DefinirTipoUsuario();
            ValidarLogin();
            ValidarSenha();

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(TipoUsuario == TipoUsuario.NaoDefinido, nameof(TipoUsuario), "Tipo de usuário não definido"));
        }
        public Usuario(Guid id, string login, string senha)
        {
            Id = id;
            Login = login;
            Senha = senha;
            TipoUsuario = DefinirTipoUsuario();
            ValidarLogin();
            ValidarSenha();

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(TipoUsuario == TipoUsuario.NaoDefinido, nameof(TipoUsuario), "Tipo de usuário não definido"));
        }

        public void AlterarId(Guid id)
        {
            Id = id;
        }

        private TipoUsuario DefinirTipoUsuario()
        {
            if (!string.IsNullOrWhiteSpace(Login))
            {
                switch (Login.Length)
                {
                    case 11:
                        return TipoUsuario.Cliente;
                    case 6:
                        return TipoUsuario.Operador;
                }
            }

            return TipoUsuario.NaoDefinido;
        }

        private void ValidarLogin()
        {
            if (!string.IsNullOrWhiteSpace(Login))
            {
                switch (Login.Length)
                {
                    case 11:
                        AddNotifications(new Cpf(Login));
                        break;
                    case 6:
                        AddNotifications(new Matricula(Login));
                        break;
                    default:
                        AddNotifications(new Contract()
                            .Requires()
                            .IsTrue(Login.Length == 6 || Login.Length == 11, nameof(Login), "Login inválido"));
                        break;
                }
            }
            else
                AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Login, nameof(Login), "Login não pode ser nulo ou branco"));
        }
        private void ValidarSenha()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrWhiteSpace(Senha, nameof(Senha), "Numero da senha não pode ser nulo ou branco")
                .HasMinLen(Senha, 8, nameof(Senha), "A senha deve conter no minimo 8 caracteres")
                .Matchs(Senha, @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", nameof(Senha), "A senha deve conter no minimo (uma letra maiúscula, uma letra minúscula e um número ou caractere especial)"));
        }

        public string Login { get; set; }
        public string Senha { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
