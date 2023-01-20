using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend_tasks.Models
{
    public class Tasks
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Nombre { get; set; }

        [Required]
        public bool Estado { get; set; }
    }
}
