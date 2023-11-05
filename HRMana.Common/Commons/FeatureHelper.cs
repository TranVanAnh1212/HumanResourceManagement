using HRMana.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Common
{
    public static class FeatureHelper
    {
        public static void ShowNotification(ref string targetMessage, ref string targetFill, string message, string fill)
        {
            targetMessage = message;
            targetFill = fill;
            NotificationEvent.Instance.ReqquestShowNotification();
        }
    }
}
