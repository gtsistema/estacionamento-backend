using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Estac.Domain.Extensions.Notifier
{
    public class Notifier : INotifier
    {
        private readonly Dictionary<string, IList<string>> _notifications;

        public Notifier()
        {
            _notifications = new Dictionary<string, IList<string>>();
        }

        bool INotifier.HasNotification => _notifications.Any();

        public ReadOnlyDictionary<string, IList<string>> GetNotifications()
        {
            return new ReadOnlyDictionary<string, IList<string>>(_notifications);
        }

        public void Handle(Notification notification)
        {
            var containsKey = _notifications.ContainsKey(notification.Key);
            if (containsKey)
            {
                _notifications[notification.Key].Add(notification.Message);
            }
            else
            {
                _notifications.Add(notification.Key, new List<string>
                {
                    notification.Message
                });
            }
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }
}