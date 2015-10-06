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

        [SetUp]
        public void Init()
        {
            _fakeConsole = Substitute.For<IConsole>();
            _taskList = new TaskList(_fakeConsole);
        }

        [Test]
        public void HandlingTasks()
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

        private void Execute(string command)
        {
            _taskList.Execute(command);
        }

    }
}