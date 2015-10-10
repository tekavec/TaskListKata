using NSubstitute;
using NUnit.Framework;

namespace TaskListKata.Tests
{
    [TestFixture]
    public class DefaultCommandShould
    {
        private DefaultCommand _defaultCommand;
        private IConsole _fakeConsole;

        [SetUp]
        public void Init()
        {
            _fakeConsole = Substitute.For<IConsole>();
            _defaultCommand = new DefaultCommand(_fakeConsole, "$&$%$");
        }

        [Test]
        public void DisplayTheCommandIsUnknownMessage()
        {
            _defaultCommand.Execute();

            _fakeConsole.Received().WriteLine("I don't know what the command \"{0}\" is.", "$&$%$");
        }
         
    }
}