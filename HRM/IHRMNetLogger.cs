using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MGT.Utilities.EventHandlers;

namespace MGT.HRM
{
    public interface IHRMNetLogger : IHRMLogger
    {

        event GenericEventHandler<string> AddressChanged;

        event GenericEventHandler<int> PortChanged;

        string Address { get; set; }

        int Port { get; set; }

    }
}
