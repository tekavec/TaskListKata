using NUnit.Framework;

namespace TaskListKata.Tests
{
    [TestFixture]
    public class AddProjectCommandShould
    {
        [Test]
        public void AddAProjectToRepository()
        {
            var projectRepository = new ProjectRepository();
            var project = new Project("secrets");
            var addProjectCommand = new AddProjectCommand(projectRepository, "secrets");

            addProjectCommand.Execute();

            Assert.That(projectRepository.AllProjects()[0], Is.EqualTo(project));
        }
    }
}