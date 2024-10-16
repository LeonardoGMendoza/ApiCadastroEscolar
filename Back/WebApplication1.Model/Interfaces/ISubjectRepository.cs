using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Model.Interfaces
{
    public interface ISubjectRepository
    {
        Subject Insert(Subject subject); // Verifique o tipo de retorno
        List<Subject> GetAll();
        Subject Get(int id);
        void Delete(int id);
        Subject Update(Subject subject);
    }

}
