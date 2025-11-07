using GestionProyectosYTareas.Application.Interfaces;
using GestionProyectosYTareas.Domain.Entities;
using GestionProyectosYTareas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionProyectosYTareas.Shared.DTOs;
using GestionProyectosYTareas.Infrastructure.Interfaces;
using XAct.Messages;

namespace GestionProyectosYTareas.Application.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _repo;

        public TaskItemService(ITaskItemRepository repo)
        {
            _repo = repo;
        }

        public async Task<PagedResponse<TaskItemDto>> GetPagedAsync(int page, int pageSize, Guid? projectId, string status, string q, string sort)
        {
            var tasks = await _repo.GetPagedAsync(page, pageSize, projectId, status, q, sort);
            var result = tasks.Select(t => new TaskItemDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status.ToString(),
                Priority = t.Priority.ToString(),
                DueDate = t.DueDate,
                AssignedTo = t.AssignedTo,
                ProjectId = t.ProjectId,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            }).ToList();

            return new PagedResponse<TaskItemDto>(result, page, pageSize, result.Count);
        }

        public async Task<TaskItemDto> GetByIdAsync(Guid id)
        {
            var t = await _repo.GetByIdAsync(id);
            if (t == null) return null;

            return new TaskItemDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status.ToString(),
                Priority = t.Priority.ToString(),
                DueDate = t.DueDate,
                AssignedTo = t.AssignedTo,
                ProjectId = t.ProjectId,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            };
        }

        public async Task<TaskItemDto> CreateAsync(TaskItemCreateDto dto)
        {
            if (dto.DueDate.HasValue && dto.DueDate.Value < DateTime.UtcNow)
                throw new ArgumentException("DueDate no puede ser en el pasado.");

            if (!Enum.TryParse<Domain.Enums.TaskStatus>(dto.Status, true, out var status))
                throw new ArgumentException("Estado inválido.");

            if (!Enum.TryParse<Domain.Enums.TaskPriority>(dto.Priority, true, out var priority))
                throw new ArgumentException("Prioridad inválida.");

            var entity = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                Priority = dto.Priority,
                DueDate = dto.DueDate,
                AssignedTo = dto.AssignedTo,
                ProjectId = dto.ProjectId
            };

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return new TaskItemDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Status = entity.Status.ToString(),
                Priority = entity.Priority.ToString()
            };
        }

        public async Task<bool> UpdateAsync(Guid id, TaskItemUpdateDto dto, byte[] rowVersion)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            // Control de concurrencia
            if (rowVersion != null && !existing.RowVersion.SequenceEqual(rowVersion))
                throw new InvalidOperationException("Concurrencia detectada.");

            existing.Title = dto.Title;
            existing.Description = dto.Description;

            // 👇 Usa tu propio enum, no el de System.Threading.Tasks
            if (Enum.TryParse<Domain.Enums.TaskStatus>(dto.Status, true, out var parsedStatus))
                existing.Status = parsedStatus.ToString();

            if (Enum.TryParse<Domain.Enums.TaskPriority>(dto.Priority, true, out var parsedPriority))
                existing.Priority = parsedPriority.ToString();

            existing.DueDate = dto.DueDate;
            existing.AssignedTo = dto.AssignedTo;
            existing.UpdatedAt = DateTime.UtcNow;

            _repo.Update(existing);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return false;

            _repo.Remove(existing);
            await _repo.SaveChangesAsync();

            return true;
        }

    }
}
