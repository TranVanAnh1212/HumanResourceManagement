using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Common.Events
{
    public class NotificationEvent
    {
        private static readonly NotificationEvent _instance = new NotificationEvent();

        public static NotificationEvent Instance => _instance;

        // Show notify
        public event EventHandler<EventArgs> ShowNotificationRequested;
        public void ReqquestShowNotification ()
        {
            ShowNotificationRequested?.Invoke(this, EventArgs.Empty);
        }

        // Hide notify
        public event EventHandler<EventArgs> HideNotificationRequested;
        public void ReqquestHideNotification()
        {
            ShowNotificationRequested?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<EventArgs> ShowPageRequested;
        public void RequestShowPage()
        {
            ShowPageRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
