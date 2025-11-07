using GestionProyectosYTareas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectosYTareas.Infrastructure.Interfaces
{
    public interface IProjecttRepository
    {
        Task<Project> GetByIdAsync(Guid id);

        Task<IEnumerable<Project>> GetPagedAsync(
            int page,
            int pageSize,
            string q,
            string sort
        );

        Task AddAsync(Project task);
        void Update(Project task);
        void Remove(Project task);
        Task SaveChangesAsync();
    }
}
