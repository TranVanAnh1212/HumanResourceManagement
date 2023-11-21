using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMana.Common.Events
{
    public static class RegisterEvent
    {
        public delegate void DataCommunicationHandler(int ID);
    }
}
