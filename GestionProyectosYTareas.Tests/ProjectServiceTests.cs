using Xunit;
using Moq;
using GestionProyectosYTareas.Application.Services;
using GestionProyectosYTareas.Domain.Entities;
using GestionProyectosYTareas.Domain.Interfaces;

namespace GestionProyectosYTareas.Tests
{
    public class ProjectServiceTests
    {
        [Fact]
        public async Task CreateAsync_ShouldSetDatesAndReturnProject()
        {
            var repo = new Mock<IProjectRepository>();
            repo.Setup(r => r.AddAsync(It.IsAny<Project>())).Returns(Task.CompletedTask);


            var service = new ProjectService(repo.Object);
            var project = new Project { Name = "Test Project" };


            var result = await service.CreateAsync(project);


            Assert.NotNull(result);
            Assert.NotEqual(default, result.CreatedAt);
            Assert.Equal(result.CreatedAt, result.UpdatedAt);
        }
    }
}