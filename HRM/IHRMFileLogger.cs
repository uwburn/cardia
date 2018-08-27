using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MGT.Utilities.EventHandlers;

namespace MGT.HRM
{
    public interface IHRMFileLogger : IHRMLogger
    {
        event GenericEventHandler<string> FileNameChanged;

        string FileName { get; set; }

    }
}
