// WebApplication1.Repository/StudentRepository.cs
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;
using WebApplication1.Model.Interfaces;
using WebApplication1.Repository.Context;

namespace WebApplication1.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentContext _context;

        public StudentRepository(StudentContext context)
        {
            _context = context;
        }

        public async Task<Student> GetAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task InsertAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            return await _context.SaveChangesAsync() > 0; // Retorna verdadeiro se a atualização for bem-sucedida
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var student = await GetAsync(id);
            if (student != null)
            {
                // Excluir associações primeiro
                var studentSubjects = await _context.StudentSubjects
                    .Where(ss => ss.StudentId == id)
                    .ToListAsync();

                _context.StudentSubjects.RemoveRange(studentSubjects); // Remove as associações

                _context.Students.Remove(student); // Remove o estudante
                return await _context.SaveChangesAsync() > 0; // Salva as mudanças e retorna verdadeiro se a exclusão for bem-sucedida
            }
            return false; // Retorna falso se o estudante não foi encontrado
        }



        //public async Task<bool> DeleteAsync(int id)
        //{
        //    var student = await GetAsync(id);
        //    if (student != null)
        //    {
        //        _context.Students.Remove(student);
        //        return await _context.SaveChangesAsync() > 0; // Retorna verdadeiro se a exclusão for bem-sucedida
        //    }
        //    return false; // Retorna falso se o estudante não foi encontrado
        //}

        public async Task<bool> CheckIfInsertedAsync(int id)
        {
            return await _context.Students.AnyAsync(s => s.Id == id); // Retorna verdadeiro se o estudante existir
        }
    }
}
