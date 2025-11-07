using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionProyectosYTareas.Shared.DTOs;
using XAct.Messages;

namespace GestionProyectosYTareas.Application.Interfaces
{
    public interface IProjectService
    {
        Task<(IEnumerable<ProjectDto> Items, int TotalItems)> GetPagedAsync(int page, int pageSize, string q, string sort);
        Task<ProjectDto> GetByIdAsync(Guid id);
        Task<ProjectDto> CreateAsync(ProjectCreateDto dto);
        Task<bool> UpdateAsync(Guid id, ProjectUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
