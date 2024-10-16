using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Model.Interfaces
{
    public interface IStudentRepository
    {
        public void Insert(Student student);
        public List<Student> GetAll();
        public Student? Get(int id);
        public void Delete(int id);
        public Student Update(Student student);
        public bool CheckIfInserted(int id);
    }
}


