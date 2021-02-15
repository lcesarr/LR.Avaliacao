using Flunt.Notifications;
using System;

namespace LR.Avaliacao.Domain.Core
{
    public class IdEntity : Notifiable
    {
        private Guid _id;
        public virtual Guid Id
        {
            get => _id;
            protected set => _id = value;
        }

        protected IdEntity() => Id = Guid.NewGuid();
    }
}
