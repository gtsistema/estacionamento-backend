using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Gp.Domain.Extensions.Notifier
{
    public interface INotifier
    {
        bool HasNotification { get; }
        ReadOnlyDictionary<string, IList<string>> GetNotifications();
        void Handle(Notification notification);
    }

    public class Notification
    {
        public string Key { get; }
        public string Message { get; }

        public Notification(string key, string message)
        {
            Message = message;
            Key = key;
        }
    }
}