using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdProject
{
    class EmptyPathException : Exception
    {
        public EmptyPathException():base("You can not enter empty Path.")
        {

        }
    }
}
