using System.Collections.Generic;
using WebApplication1.Model;

namespace WebApplication1.Model.Interfaces
{
    public interface ISubjectBusiness
    {
        Subject Insert(Subject subject);
        List<Subject> GetAll();
        Subject Get(int id);
        void Delete(int id);
        Subject Update(Subject subject);
    }
}

