using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdProject
{
    class AllSymbolException : Exception
    {
        public AllSymbolException() : base("Path can not contain only the symbols ' . / \\ '")
        {

        }
    }
}
