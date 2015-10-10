using NSubstitute;
using NUnit.Framework;

namespace TaskListKata.Tests
{
    [TestFixture]
    public class SetDoneCommandShould
    {

        private IConsole _fakeConsole;
        private Project _project;
        private Task _task;
        private TaskRepository _taskRepository;

        [SetUp]
        public void Init()
        {
            _fakeConsole = Substitute.For<IConsole>();
            _project = new Project("secrets");
            _task = new Task("test", _project);
            _taskRepository = new TaskRepository();
            _taskRepository.Add(_task);
        }

        [Test]
        public void check_a_task_that_exists()
        {

            var checkTaskCommand = new SetDoneCommand(_fakeConsole, _taskRepository, _task.Id.ToString(), true);

            checkTaskCommand.Execute();

            Assert.That(_task.Done, Is.True);
        }

        [Test]
        public void uncheck_a_task_that_exists()
        {
            var checkTaskCommand = new SetDoneCommand(_fakeConsole, _taskRepository, _task.Id.ToString(), false);

            checkTaskCommand.Execute();

            Assert.That(_task.Done, Is.False);
        }

        [Test]
        public void shows_a_message_if_task_is_not_found()
        {
            var checkTaskCommand = new SetDoneCommand(_fakeConsole, _taskRepository, "not_there", false);

            checkTaskCommand.Execute();

            _fakeConsole.Received().WriteLine("Could not find a task with an ID of {0}.", "not_there");

        }
    }
}