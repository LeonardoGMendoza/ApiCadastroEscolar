using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json.Serialization;

namespace WebApplication1.Model
{
    public class StudentSubject
    {
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        [JsonIgnore]
        public Student Student { get; set; }
        public Subject Subject { get; set; }

        public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
        {
            public void Configure(EntityTypeBuilder<StudentSubject> builder)
            {
                // Definir a chave composta
                builder.HasKey(ss => new { ss.StudentId, ss.SubjectId });

                // Configurar o relacionamento com Student
                builder.HasOne(ss => ss.Student)
                       .WithMany(s => s.StudentSubjects)
                       .HasForeignKey(ss => ss.StudentId)
                       .OnDelete(DeleteBehavior.Cascade);

                // Configurar o relacionamento com Subject
                builder.HasOne(ss => ss.Subject)
                       .WithMany(s => s.StudentSubjects)
                       .HasForeignKey(ss => ss.SubjectId)
                       .OnDelete(DeleteBehavior.Cascade);
            }
        }
    }
}
