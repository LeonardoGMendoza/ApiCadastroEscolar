using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Model.Interfaces
{
    public interface IStudentBusiness
    {
        public Student Insert(Student student);
        public List<Student> GetAll();
        public Student Get(int id);
        public bool Delete(int id);
        public bool Update(Student student);
        public int CalcularSituacao(int id);
    }
}

