using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model.Dto
{
    public class SubjectDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da disciplina é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da disciplina deve ter no máximo 100 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "O nome da disciplina pode conter apenas letras, números e espaços.")]
        public string SubjectName { get; set; }
    }
}
