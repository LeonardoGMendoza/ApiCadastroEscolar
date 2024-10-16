using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string StudentName { get; set; }

        public DateTime? DateOfBirth { get; set; } // Nullable, se não for obrigatório

        public decimal Height { get; set; } // Usar decimal para manter precisão
        public float Weight { get; set; } // Aqui está como float
        public int Nota { get; set; } // Mantenha como int

        public ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
    }
}
