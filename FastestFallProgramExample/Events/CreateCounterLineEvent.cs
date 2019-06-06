using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastestFallProgramExample.Events
{
    public class CreateCounterLineEvent : PubSubEvent<CreateCounterLineEventArgs>
    {
    }
    public class CreateCounterLineEventArgs
    {
        public double ImegeSize { get; set; }
        public double Range { get; set; }
        public int Step { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}
