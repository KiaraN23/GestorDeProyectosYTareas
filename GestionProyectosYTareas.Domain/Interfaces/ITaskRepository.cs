using GestionProyectosYTareas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectosYTareas.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetPagedAsync(Guid? projectId, string status, string q, int page, int pageSize, string sort);
        Task<TaskItem> GetByIdAsync(Guid id);
        Task AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
        Task DeleteAsync(TaskItem task);
        Task<int> CountAsync(Guid? projectId, string status, string q);
    }
}
