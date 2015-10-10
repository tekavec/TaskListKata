namespace TaskListKata
{
    public class AddTaskCommand : ITaskCommand
    {
        private readonly TaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly string _remainingCommandLine;
        private readonly IConsole _console;

        public AddTaskCommand(
            IConsole console, 
            TaskRepository taskRepository, 
            IProjectRepository projectRepository, 
            string remainingCommandLine)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _remainingCommandLine = remainingCommandLine;
            _console = console;
        }

        public void Execute()
        {
            string[] subcommandRest = _remainingCommandLine.Split(" ".ToCharArray(), 2);
            AddTask(subcommandRest[0], subcommandRest[1]);
        }

        private void AddTask(string projectName, string description)
        {
            var project = _projectRepository.FindProjectByName(projectName);
            if (project == null)
            {
                _console.WriteLine("Could not find a project with the name \"{0}\".", projectName);
                return;
            }
            _taskRepository.Add(new Task(description, project));
        }

    }
}