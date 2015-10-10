namespace TaskListKata
{
    public class SetDoneCommand : ITaskCommand
    {
        private readonly IConsole _console;
        private readonly TaskRepository _taskRepository;
        private readonly string _remainingCommandLine;
        private readonly bool _done;

        public SetDoneCommand(
            IConsole console,
            TaskRepository taskRepository, 
            string remainingCommandLine, 
            bool done)
        {
            _console = console;
            _taskRepository = taskRepository;
            _remainingCommandLine = remainingCommandLine;
            _done = done;
        }

        public void Execute()
        {
            Task identifiedTask = _taskRepository.FindTaskById(_remainingCommandLine);
            if (identifiedTask == null)
            {
                _console.WriteLine("Could not find a task with an ID of {0}.", _remainingCommandLine);
                return;
            }
            identifiedTask.Done = _done;
        }
    }
}