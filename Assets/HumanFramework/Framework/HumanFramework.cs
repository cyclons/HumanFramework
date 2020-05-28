using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HumanFramework.Event;
using HumanFramework.IOC;

namespace HumanFramework.Framework
{
    public class GlobalModule
    {
        public static ISignalContainer GlobalSignalContainer = new SignalContainer();
        public static IInjectionContainer GlobalInjectionContainer = new InjectionContainer();
    }
}
