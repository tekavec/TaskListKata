namespace TaskListKata
{
    public class ShowCommand : ITaskCommand
    {
        private readonly IConsole _console;
        private readonly TaskRepository _taskRepository;

        public ShowCommand(IConsole console, TaskRepository taskRepository)
        {
            _console = console;
            _taskRepository = taskRepository;
        }

        public void Execute()
        {
            if (_taskRepository.IsNullOrEmpty())
            {
                return;
            }
            foreach (var projectWithTasks in _taskRepository.TasksByProjects())
            {
                _console.WriteLine(projectWithTasks.Key.Name());
                foreach (Task task in projectWithTasks.Value)
                {
                    _console.WriteLine("    [{0}] {1}: {2}", (task.Done ? 'x' : ' '), task.Id, task.Description);
                }
                _console.WriteLine();
            }

        }
    }
}