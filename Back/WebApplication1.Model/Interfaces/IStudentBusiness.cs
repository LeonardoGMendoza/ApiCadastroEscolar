// WebApplication1.Model.Interfaces/IStudentBusiness.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Model.Interfaces
{
    public interface IStudentBusiness
    {
        Task<Student> InsertAsync(Student student);
        Task<List<Student>> GetAllAsync();
        Task<Student> GetAsync(int id);
        Task<bool> UpdateAsync(Student student);
        Task<bool> DeleteAsync(int id); // Verifique se está assim
        Task<int> CalcularSituacaoAsync(int id);
    }
}
