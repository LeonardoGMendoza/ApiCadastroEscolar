using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do aluno é obrigatório.")]
        public string StudentName { get; set; }

        public DateTime? DateOfBirth { get; set; } // Nullable, se não for obrigatório

        [Range(0, 300, ErrorMessage = "A altura deve estar entre 0 e 300 cm.")]
        public decimal Height { get; set; } // Usar decimal para manter precisão

        [Range(0, 300, ErrorMessage = "O peso deve estar entre 0 e 300 kg.")]
        public float Weight { get; set; } // Aqui está como float

        public int Nota { get; set; } // Mantenha como int

        public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
    }
}
