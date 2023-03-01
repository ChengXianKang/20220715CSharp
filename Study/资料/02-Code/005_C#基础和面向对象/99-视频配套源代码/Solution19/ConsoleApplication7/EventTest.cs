using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication7
{
    class EventTest
    {
        public delegate void MyEventHandler();
        public event MyEventHandler ValueChanged;

        private int value;
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                ValueChanged();
                this.value = value;
            }
        }
    }
}
