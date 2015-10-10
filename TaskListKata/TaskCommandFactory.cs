namespace TaskListKata
{
    public class TaskCommandFactory
    {
        private readonly IConsole _console;
        private readonly ProjectRepository _projectRepository;
        private readonly TaskRepository _taskRepository;

        public TaskCommandFactory(
            IConsole console, 
            ProjectRepository projectRepository, 
            TaskRepository taskRepository)
        {
            _console = console;
            _projectRepository = projectRepository;
            _taskRepository = taskRepository;
        }

        public ITaskCommand CreateCommand(string commandLine)
        {
            string[] commandRest = commandLine.Split(" ".ToCharArray(), 2);
            string commandName = commandRest[0];
            if (commandName == "add")
            {
                string[] subcommandRest = commandRest[1].Split(" ".ToCharArray(), 2);
                string subcommand = subcommandRest[0];
                commandName += subcommand;
            }

            if (commandName == "show")
            {
                return new ShowCommand(_console, _taskRepository);
            }
            if (commandName == "addtask")
            {
                string[] subcommandRest = commandLine.Split(" ".ToCharArray(), 3);
                return new AddTaskCommand(_console, _taskRepository, _projectRepository, subcommandRest[2]);
            }
            if (commandName == "addproject")
            {
                string[] subcommandRest = commandLine.Split(" ".ToCharArray(), 3);
                return new AddProjectCommand(_projectRepository, subcommandRest[2]);
            }
            if (commandName == "check")
            {
                string[] subcommandRest = commandLine.Split(" ".ToCharArray(), 2);
                return new SetDoneCommand(_console, _taskRepository, subcommandRest[1], true);
            }
            if (commandName == "uncheck")
            {
                string[] subcommandRest = commandLine.Split(" ".ToCharArray(), 2);
                return new SetDoneCommand(_console, _taskRepository, subcommandRest[1], false);
            }
            return new DefaultCommand(_console, commandName);
        }
    }
}