using Flunt.Notifications;
using System.Collections.Generic;

namespace LR.Avaliacao.Application.Resultado
{
    public class Erro
    {
        public List<string> Erros { get; } = new List<string>();

        public Erro(string erro)
        {
            Erros.Add(erro);
        }

        public Erro(IEnumerable<string> erros)
        {
            Erros.AddRange(erros);
        }

        public Erro(IReadOnlyCollection<Notification> notifications)
        {
            foreach (var notification in notifications)
            {
                Erros.Add(notification.Message);
            }
        }
    }
}
