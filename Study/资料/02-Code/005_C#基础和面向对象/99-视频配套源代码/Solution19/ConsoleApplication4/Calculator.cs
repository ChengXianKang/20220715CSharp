using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Calculator
    {
        public int Num { get; set; }
        public void Add(int n)
        {
            this.Num += n;
        }
        public void Sub(int n)
        {
            this.Num -= n;
        }
        public void Mul(int n)
        {
            this.Num *= n;
        }
        public void Div(int n)
        {
            this.Num /= n;
        }
    }
}
