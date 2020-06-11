using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathLibrary;

namespace TestApplication1.ViewModel
{
    public class TestDll
    {
        public TestDll()
        {
            int Result = MathOperations.Addition(10, 20);
        }
    }
}
