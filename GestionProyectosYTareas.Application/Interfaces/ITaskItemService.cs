using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionProyectosYTareas.Shared.DTOs;
using XAct.Messages;

namespace GestionProyectosYTareas.Application.Interfaces
{
    public interface ITaskItemService
    {
        Task<PagedResponse<TaskItemDto>> GetPagedAsync(int page, int pageSize, Guid? projectId, string status, string q, string sort);
        Task<TaskItemDto> GetByIdAsync(Guid id);
        Task<TaskItemDto> CreateAsync(TaskItemCreateDto dto);
        Task<bool> UpdateAsync(Guid id, TaskItemUpdateDto dto, byte[] rowVersion);
        Task<bool> DeleteAsync(Guid id);
    }
}
