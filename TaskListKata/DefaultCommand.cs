namespace TaskListKata
{
    public class DefaultCommand : ITaskCommand
    {
        private readonly IConsole _console;
        private readonly string _commandName;

        public DefaultCommand(IConsole console, string commandName)
        {
            _console = console;
            _commandName = commandName;
        }

        public void Execute()
        {
            _console.WriteLine("I don't know what the command \"{0}\" is.", _commandName);
        }
    }
}