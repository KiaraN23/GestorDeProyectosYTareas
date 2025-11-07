using GestionProyectosYTareas.Application.Services;
using GestionProyectosYTareas.Infrastructure.Interfaces;
using GestionProyectosYTareas.Shared.DTOs;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProyectosYTareas.Tests
{
    public class TaskItemServiceTests
    {
        [Fact]
        public async Task NoDebePermitirFechaPasada()
        {
            var repoMock = new Mock<ITaskItemRepository>();
            var service = new TaskItemService(repoMock.Object);

            var dto = new TaskItemCreateDto
            {
                Title = "Tarea inválida",
                ProjectId = Guid.NewGuid(),
                Status = "Todo",
                Priority = "Medium",
                DueDate = DateTime.UtcNow.AddDays(-1)
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(dto));
        }
    }
}
