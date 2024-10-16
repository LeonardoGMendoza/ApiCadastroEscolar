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

        public void Insert(Student student)
        {
            _context.Add(student);
            _context.SaveChanges();
        }

        public List<Student> GetAll()
        {
            return _context.Students.ToList();
        }


        public void Delete(int id)
        {
            Student student = new() { Id = id };

            _context.Attach(student);
            _context.Remove(student);

            _context.SaveChanges();

        }

        public Student Update(Student student)
        {
            _context.Update(student);
            _context.SaveChanges();

            return student;
        }

        public bool CheckIfInserted(int id)
        {
            return _context.Students.Any(b => b.Id.Equals(id));
        }

        public Student? Get(int id)
        {
            return _context.Students.FirstOrDefault(b => b.Id.Equals(id));
        }
    }
}
