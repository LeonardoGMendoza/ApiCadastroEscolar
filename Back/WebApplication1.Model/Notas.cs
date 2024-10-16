using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model
{
    public class Notas
    {
        [Key]
        public int Id { get; set; }

        // Adicione a chave estrangeira para referenciar o estudante
        [ForeignKey("Student")]
        public int StudentId { get; set; }

        // Propriedade para armazenar a nota
        public decimal Grade { get; set; }

        // Relacionamento com o estudante
        public virtual Student Student { get; set; }
    }
}
