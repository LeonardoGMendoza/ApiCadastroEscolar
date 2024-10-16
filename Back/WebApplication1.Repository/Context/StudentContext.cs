using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Repository.Context
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentSubject.StudentSubjectConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
