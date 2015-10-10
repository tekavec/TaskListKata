using NSubstitute;
using NUnit.Framework;

namespace TaskListKata.Tests
{
    [TestFixture]
    public class TaskListFeature
    {
        public const string Prompt = "> ";
        private IConsole _fakeConsole;
        private TaskList _taskList;
        private ProjectRepository _projectRepository;
        private TaskRepository _taskRepository;

        [SetUp]
        public void Init()
        {
            _fakeConsole = Substitute.For<IConsole>();
            _projectRepository = new ProjectRepository();
            _taskRepository = new TaskRepository();
            _taskList = new TaskList(_fakeConsole, _projectRepository, _taskRepository);
        }

        [Test]
        public void AddProjectsWithTasksAndCheckSomeTasksAndShowATaskPerProjectList()
        {
            Execute("show");

            Execute("add project secrets");
            Execute("add task secrets Eat more donuts.");
            Execute("add task secrets Destroy all humans.");
            Execute("show");

            Execute("add project training");
            Execute("add task training Four Elements of Simple Design");
            Execute("add task training SOLID");
            Execute("add task training Coupling and Cohesion");
            Execute("add task training Primitive Obsession");
            Execute("add task training Outside-In TDD");
            Execute("add task training Interaction-Driven Design");

            Execute("check 1");
            Execute("check 3");
            Execute("check 5");
            Execute("check 6");

            Execute("show");
            Received.InOrder(() =>
                {
                    _fakeConsole.WriteLine("secrets");
                    _fakeConsole.WriteLine("    [{0}] {1}: {2}", ' ', 1L, "Eat more donuts.");
                    _fakeConsole.WriteLine("    [{0}] {1}: {2}", ' ', 2L, "Destroy all humans.");
                    _fakeConsole.WriteLine();

                    _fakeConsole.WriteLine("secrets");
                    _fakeConsole.WriteLine("    [{0}] {1}: {2}", 'x', 1L, "Eat more donuts.");
                    _fakeConsole.WriteLine("    [{0}] {1}: {2}", ' ', 2L, "Destroy all humans.");
                    _fakeConsole.WriteLine();
                    _fakeConsole.WriteLine("training");
                    _fakeConsole.WriteLine("    [{0}] {1}: {2}", 'x', 3L, "Four Elements of Simple Design");
                    _fakeConsole.WriteLine("    [{0}] {1}: {2}", ' ', 4L, "SOLID");
                    _fakeConsole.WriteLine("    [{0}] {1}: {2}", 'x', 5L, "Coupling and Cohesion");
                    _fakeConsole.WriteLine("    [{0}] {1}: {2}", 'x', 6L, "Primitive Obsession");
                    _fakeConsole.WriteLine("    [{0}] {1}: {2}", ' ', 7L, "Outside-In TDD");
                    _fakeConsole.WriteLine("    [{0}] {1}: {2}", ' ', 8L, "Interaction-Driven Design");
                    _fakeConsole.WriteLine();
                }
            );
            Execute("quit");
        }

        [Test]
        public void UncheckExistingTasks()
        {
            Execute("add project secrets");
            Execute("add task secrets Eat more donuts.");
            Execute("check 1");
            Execute("show");
            Execute("uncheck 1");
            Execute("show");

            Received.InOrder(() =>
            {
                _fakeConsole.WriteLine("secrets");
                _fakeConsole.WriteLine("    [{0}] {1}: {2}", 'x', 1L, "Eat more donuts.");
                _fakeConsole.WriteLine();

                _fakeConsole.WriteLine("secrets");
                _fakeConsole.WriteLine("    [{0}] {1}: {2}", ' ', 1L, "Eat more donuts.");
                _fakeConsole.WriteLine();
            });
        }

        [Test]
        public void CheckNonexistentTasks()
        {
            Execute("add project secrets");
            Execute("add task secrets Eat more donuts.");
            Execute("check 2");

            Received.InOrder(() =>
            {
                _fakeConsole.WriteLine("Could not find a task with an ID of {0}.", 2);
            });
        }

        [Test]
        public void AddTaskToNonexistentProject()
        {
            Execute("add project secrets");
            Execute("add task training Eat more donuts.");

            _fakeConsole.Received().WriteLine("Could not find a project with the name \"{0}\".", "training");
        }

        [Test]
        public void ShowHelpContent()
        {
            Execute("help");

            Received.InOrder(() =>
            {
                _fakeConsole.WriteLine("Commands:");
                _fakeConsole.WriteLine("  show");
                _fakeConsole.WriteLine("  add project <project name>");
                _fakeConsole.WriteLine("  add task <project name> <task description>");
                _fakeConsole.WriteLine("  check <task ID>");
                _fakeConsole.WriteLine("  uncheck <task ID>");
                _fakeConsole.WriteLine();

            });
        }

        private void Execute(string command)
        {
            _taskList.ParseCommandLine(command);
        }

    }
}