namespace TaskListKata
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var console = new RealConsole();
            var projectRepository = new ProjectRepository();
            var taskRepository = new TaskRepository();
            var taskList = new TaskList(console, projectRepository, taskRepository);
            taskList.Run();
        }
    }
}