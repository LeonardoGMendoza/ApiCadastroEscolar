using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Model.Exceptions
{
    public class InvalidWebApplication1Exception : Exception
    {
        public InvalidWebApplication1Exception(string mensagem) : base(mensagem) { }
    }
}
