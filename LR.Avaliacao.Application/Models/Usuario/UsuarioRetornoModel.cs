using LR.Avaliacao.Domain.Enum;
using System;

namespace LR.Avaliacao.Application.Models.Usuario
{
    public class UsuarioRetornoModel : UsuarioModel
    {
        public Guid Id { get; set; }
        public TipoUsuario TipoUsuario { get { return ObterTipoUsuario(); } }

        private TipoUsuario ObterTipoUsuario()
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
    }
}
