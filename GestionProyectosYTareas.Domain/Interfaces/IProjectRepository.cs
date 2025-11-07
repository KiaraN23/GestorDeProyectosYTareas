using GestionProyectosYTareas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectosYTareas.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetPagedAsync(int page, int pageSize, string search, string sort);
        Task<Project> GetByIdAsync(Guid id);
        Task AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(Project project);
        Task<int> CountAsync(string search);
        Task SaveChangesAsync();
    }
}
