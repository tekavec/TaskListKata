namespace TaskListKata
{
    public class AddProjectCommand : ITaskCommand
    {
        private readonly ProjectRepository _projectRepository;
        private readonly string _projectName;

        public AddProjectCommand(ProjectRepository projectRepository, string projectName)
        {
            _projectRepository = projectRepository;
            _projectName = projectName;
        }

        public void Execute()
        {
           _projectRepository.Add(new Project(_projectName));
        }
    }
}