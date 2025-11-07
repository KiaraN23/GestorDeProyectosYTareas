using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionProyectosYTareas.Domain.Entities;
using GestionProyectosYTareas.Domain.Interfaces;
using GestionProyectosYTareas.Application.Interfaces;
using GestionProyectosYTareas.Shared.DTOs;
using XAct.Messages;

namespace GestionProyectosYTareas.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repo;
        public ProjectService(IProjectRepository repo) => _repo = repo;


        public async Task<(IEnumerable<ProjectDto> Items, int TotalItems)> GetPagedAsync(int page, int pageSize, string sort, string q)
        {
            var projects = await _repo.GetPagedAsync(page, pageSize, q, sort);
            var total = await _repo.CountAsync(q);

            var dtoList = projects.Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();

            return (dtoList, total);
        }


        public async Task<ProjectDto> GetByIdAsync(Guid id) 
        {
            var project = await _repo.GetByIdAsync(id);
            if (project == null) return null;

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt,
                UpdatedAt = project.UpdatedAt
            };
        }

        public async Task<ProjectDto> CreateAsync(ProjectCreateDto project)
        {
            var entity = new Project
            {
                Id = Guid.NewGuid(),
                Name = project.Name,
                Description = project.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return new ProjectDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }


        public async Task<bool> UpdateAsync(Guid id, ProjectUpdateDto updated)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = updated.Name;
            existing.Description = updated.Description;
            existing.UpdatedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(existing);
            await _repo.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;
            await _repo.DeleteAsync(existing);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
