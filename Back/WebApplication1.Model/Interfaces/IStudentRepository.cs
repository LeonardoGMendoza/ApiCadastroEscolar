// WebApplication1.Model.Interfaces/IStudentRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Model.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> GetAsync(int id);
        Task<List<Student>> GetAllAsync();
        Task InsertAsync(Student student);
        Task<bool> UpdateAsync(Student student); // Deve ser Task<bool>
        Task<bool> DeleteAsync(int id); // Deve ser Task<bool>
        Task<bool> CheckIfInsertedAsync(int id); // Adicione esta linha
    }
}
