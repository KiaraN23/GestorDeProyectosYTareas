using GestionProyectosYTareas.Domain.Entities;
using GestionProyectosYTareas.Domain.Interfaces;
using GestionProyectosYTareas.Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using GestionProyectosYTareas.Infrastructure.Interfaces;

namespace GestionProyectosYTareas.Infrastructure.Repositories
{
    public class ProjectRepository : IProjecttRepository
    {
        private readonly GestionPTDbContext _db;
        public ProjectRepository(GestionPTDbContext db) => _db = db;


        public async Task<IEnumerable<Project>> GetPagedAsync(int page, int pageSize, string search, string sort)
        {
            var query = _db.Projects.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));


            switch (sort)
            {
                case "name":
                    query = query.OrderBy(p => p.Name);
                    break;
                case "-createdAt":
                    query = query.OrderByDescending(p => p.CreatedAt);
                    break;
                default:
                    query = query.OrderBy(p => p.Id);
                    break;
            }


            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }


        public async Task<Project> GetByIdAsync(Guid id) => await _db.Projects.Include(p => p.Tasks).FirstOrDefaultAsync(p => p.Id == id);
        public async Task AddAsync(Project project) { _db.Projects.Add(project); await _db.SaveChangesAsync(); }
        public void Update(Project project)
        {
            _db.Projects.Update(project);
        }

        public void Remove(Project project)
        {
            _db.Projects.Remove(project);
        }

        public async Task DeleteAsync(Project project) { _db.Projects.Remove(project); await _db.SaveChangesAsync(); }
        public async Task<int> CountAsync(string search) => await _db.Projects.CountAsync(p => string.IsNullOrEmpty(search) || p.Name.Contains(search));
        public async Task SaveChangesAsync(){ await _db.SaveChangesAsync(); }
    }
}
