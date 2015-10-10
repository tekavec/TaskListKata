using NUnit.Framework;

namespace TaskListKata.Tests
{
    [TestFixture]
    public class TaskCommandFactoryShould
    {
        private TaskCommandFactory _taskCommandFactory;
        private IConsole _console;
        private ProjectRepository _projectRepository;
        private TaskRepository _taskRepository;

        [SetUp]
        public void Init()
        {
            _taskRepository = new TaskRepository();
            _console = new RealConsole();
            _projectRepository = new ProjectRepository();
            _taskCommandFactory = new TaskCommandFactory(_console, _projectRepository, _taskRepository);
        }

        [Test]
        public void CreateADefaultCommand()
        {
            var command = _taskCommandFactory.CreateCommand("#$%$$%&");

            Assert.That(command, Is.TypeOf(typeof(DefaultCommand)));
        }

        [Test]
        public void CreateAShowCommand()
        {
            var command = _taskCommandFactory.CreateCommand("show");

            Assert.That(command, Is.TypeOf(typeof(ShowCommand)));
        }

        [Test]
        public void CreateAnAddTaskCommand()
        {
            var command = _taskCommandFactory.CreateCommand("add task secrets opo");

            Assert.That(command, Is.TypeOf(typeof(AddTaskCommand)));
        }

        [Test]
        public void CreateAnAddProjectCommand()
        {
            var command = _taskCommandFactory.CreateCommand("add project secrets");

            Assert.That(command, Is.TypeOf(typeof(AddProjectCommand)));
        }

        [Test]
        public void create_a_SetDoneCommand_for_checking_items()
        {
            var command = _taskCommandFactory.CreateCommand("check 1");

            Assert.That(command, Is.TypeOf(typeof(SetDoneCommand)));
        }

        [Test]
        public void create_a_SetDoneCommand_for_unchecking_items()
        {
            var command = _taskCommandFactory.CreateCommand("uncheck 1");

            Assert.That(command, Is.TypeOf(typeof(SetDoneCommand)));
        }
    }
}