using GestionProyectosYTareas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionProyectosYTareas.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using GestionProyectosYTareas.Infrastructure.Interfaces;

namespace GestionProyectosYTareas.Infrastructure.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly GestionPTDbContext _context;

        public TaskItemRepository(GestionPTDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> GetByIdAsync(Guid id)
        {
            return await _context.Tasks.Include(t => t.Project)
                                       .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TaskItem>> GetPagedAsync(int page, int pageSize, Guid? projectId, string status, string q, string sort)
        {
            var query = _context.Tasks.AsQueryable();

            if (projectId.HasValue)
                query = query.Where(t => t.ProjectId == projectId);

            if (!string.IsNullOrEmpty(status) && Enum.TryParse(status, true, out Domain.Enums.TaskStatus parsedStatus))
                query = query.Where(t => t.Status == parsedStatus.ToString());

            if (!string.IsNullOrEmpty(q))
                query = query.Where(t => t.Title.Contains(q) || t.Description.Contains(q));


            switch (sort)
            {
                case "title":
                    query = query.OrderBy(t => t.Title);
                    break;
                case "-dueDate":
                    query = query.OrderByDescending(t => t.DueDate);
                    break;
                default:
                    query = query.OrderBy(t => t.CreatedAt);
                    break;
            }

            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task AddAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public void Update(TaskItem task)
        {
            _context.Tasks.Update(task);
        }

        public void Remove(TaskItem task)
        {
            _context.Tasks.Remove(task);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
