using System;

namespace GestionProyectosYTareas.Shared.DTOs
{
    public class TaskItemCreateDto
    {
        public Guid ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "Todo";
        public string Priority { get; set; } = "Medium";
        public DateTime? DueDate { get; set; }
        public string AssignedTo { get; set; }
    }
}
