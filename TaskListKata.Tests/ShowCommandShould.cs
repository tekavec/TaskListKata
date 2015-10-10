using NSubstitute;
using NUnit.Framework;

namespace TaskListKata.Tests
{
    [TestFixture]
    public class ShowCommandShould
    {
        private IConsole _fakeConsole;
        private TaskRepository _taskRepository;
        private ShowCommand _showCommand;

        [SetUp]
        public void Init()
        {
            _taskRepository = new TaskRepository();
            var project = new Project("secrets");
            var task = new Task("Eat more donuts.", project) {Id = 1L};
            _taskRepository.Add(task);

            _fakeConsole = Substitute.For<IConsole>();
            _showCommand = new ShowCommand(_fakeConsole, _taskRepository);
        }

        [Test]
        public void ShowProjectListWithTasks()
        {
            _showCommand.Execute();

            Received.InOrder(() =>
            {
                _fakeConsole.WriteLine("secrets");
                _fakeConsole.WriteLine("    [{0}] {1}: {2}", ' ', 1L, "Eat more donuts.");
                _fakeConsole.WriteLine();
            });
        }

    }
}