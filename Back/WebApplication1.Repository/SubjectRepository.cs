using WebApplication1.Model;
using WebApplication1.Model.Interfaces;
using WebApplication1.Repository.Context;

namespace WebApplication1.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly StudentContext _context;

        public SubjectRepository(StudentContext context)
        {
            _context = context;
        }

        // Método Insert que retorna o objeto inserido
        public Subject Insert(Subject subject)
        {
            if (subject == null) throw new ArgumentNullException(nameof(subject));

            _context.Subjects.Add(subject);
            _context.SaveChanges(); // Salve as alterações no banco de dados
            return subject; // Retorne o objeto inserido
        }

        // Obtém todas as matérias
        public List<Subject> GetAll()
        {
            return _context.Subjects.ToList();
        }

        // Obtém uma matéria pelo ID
        public Subject Get(int id)
        {
            return _context.Subjects.FirstOrDefault(s => s.Id == id);
        }

        // Exclui uma matéria pelo ID
        public void Delete(int id)
        {
            var subject = new Subject { Id = id };
            _context.Attach(subject);
            _context.Remove(subject);
            _context.SaveChanges();
        }

        // Atualiza uma matéria e retorna o objeto atualizado
        public Subject Update(Subject subject)
        {
            _context.Update(subject);
            _context.SaveChanges();
            return subject;
        }
    }
}
