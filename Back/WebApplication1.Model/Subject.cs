using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model
{
    public class Subject
    {
        public int Id { get; set; } // Identificador único da disciplina

        [Required] // Indica que o nome da disciplina é obrigatório
        public string SubjectName { get; set; } // Nome da disciplina

        public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>(); // Relacionamento com a tabela de associação
    }
}
