using NSubstitute;
using NUnit.Framework;

namespace TaskListKata.Tests
{
    [TestFixture]
    public class AddTaskCommandShould
    {
        private IProjectRepository _projectRepository;
        private TaskRepository _taskRepository;
        private IConsole _console;
        private AddTaskCommand _addTaskCommand;

        [SetUp]
        public void Init()
        {
            _taskRepository = Substitute.For<TaskRepository>();
            _projectRepository = Substitute.For<IProjectRepository>();
            _console = Substitute.For<IConsole>();
            _addTaskCommand = new AddTaskCommand(_console, _taskRepository, _projectRepository, "secrets Eat more donuts.");
        }

        [Test]
        public void add_a_task_to_the_task_repository()
        {
            var project = new Project("secrets");
            var addTaskCommand = new AddTaskCommand(_console, _taskRepository, _projectRepository, "secrets Eat more donuts.");
            var task = new Task("Eat more donuts.", project);
            _projectRepository.FindProjectByName("secrets").Returns(project);
            addTaskCommand.Execute();

            _taskRepository.Received().Add(task);
        }

        [Test]
        public void notify_that_project_was_not_found_when_trying_to_add_an_task_to_nonexistent_project()
        {
            var projectRepository = new ProjectRepository();
            projectRepository.Add(new Project("training"));

            _addTaskCommand.Execute();

            _console.Received().WriteLine("Could not find a project with the name \"{0}\".", "secrets");
        }
    }
}