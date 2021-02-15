using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace LR.Avaliacao.Application.Resultado
{
    public class Retorno : Notifiable
    {
        public bool Success { get { return !Notifications.Any(); } }
        public bool Alterado { get; }

        protected Retorno()
        {
        }

        protected Retorno(Notification notification)
        {
            AddNotification(notification);
        }

        protected Retorno(IReadOnlyCollection<Notification> notifications)
        {
            AddNotifications(notifications);
        }

        protected Retorno(bool alterado)
        {
            Alterado = alterado;
        }

        public static Retorno Ok()
        {
            return new Retorno();
        }

        public static Retorno Ok(bool alterado)
        {
            return new Retorno(alterado);
        }

        public static Retorno Error(IReadOnlyCollection<Notification> notifications)
        {
            return new Retorno(notifications);
        }

        public static Retorno Error(Notification notifications)
        {
            return new Retorno(notifications);
        }
    }

    public class Retorno<T> : Notifiable where T : class
    {
        public bool Success { get { return !Notifications.Any(); } }
        public T Object { get; }
        public bool Completed { get; }

        private Retorno(T obj, bool completed)
        {
            Completed = completed;
            Object = obj;
        }

        private Retorno(bool completed = false)
        {
            Completed = completed;
        }

        private Retorno(IReadOnlyCollection<Notification> notifications)
        {
            Object = null;
            AddNotifications(notifications);
        }

        private Retorno(Notification notification)
        {
            AddNotification(notification);
        }

        public static Retorno<T> Ok(T obj, bool completed = true)
        {
            return new Retorno<T>(obj, completed);
        }

        public static Retorno<T> Ok(bool completed = true)
        {
            return new Retorno<T>(completed);
        }

        public static Retorno<T> Error(IReadOnlyCollection<Notification> notifications)
        {
            return new Retorno<T>(notifications);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public static Retorno<T> Error(Notification notification)
        {
            return new Retorno<T>(notification);
        }
    }
}