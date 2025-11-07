using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GestionProyectosYTareas.Domain.Entities
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required, MinLength(3)]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
