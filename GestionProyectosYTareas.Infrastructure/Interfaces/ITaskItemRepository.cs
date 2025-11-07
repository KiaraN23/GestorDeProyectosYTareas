using GestionProyectosYTareas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectosYTareas.Infrastructure.Interfaces
{
    public interface ITaskItemRepository
    {
        Task<TaskItem> GetByIdAsync(Guid id);

        Task<IEnumerable<TaskItem>> GetPagedAsync(
            int page,
            int pageSize,
            Guid? projectId,
            string status,
            string q,
            string sort
        );

        Task AddAsync(TaskItem task);
        void Update(TaskItem task);
        void Remove(TaskItem task);
        Task SaveChangesAsync();
    }
}
