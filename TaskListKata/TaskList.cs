using System.Collections.Generic;
using System.Linq;

namespace TaskListKata
{
    public sealed class TaskList
    {
        private readonly IConsole _console;
        private readonly ProjectRepository _projectRepository;
        private readonly TaskCommandFactory _taskCommandFactory;
        private const string QuitCommandName = "quit";

        private readonly IDictionary<string, IList<Task>> _tasks = new Dictionary<string, IList<Task>>();

        private long _lastId;

        public TaskList(IConsole console, ProjectRepository projectRepository)
        {
            _console = console;
            _projectRepository = projectRepository;
            _taskCommandFactory = new TaskCommandFactory(console, projectRepository);
        }


        public void Run()
        {
            while (true)
            {
                _console.Write("> ");
                string commandLine = _console.ReadLine();
                if (commandLine == QuitCommandName)
                {
                    break;
                }
                ParseCommandLine(commandLine);
            }
        }

        public void ParseCommandLine(string commandLine)
        {
            string[] commandRest = commandLine.Split(" ".ToCharArray(), 2);
            string commandName = commandRest[0];
            var command = _taskCommandFactory.CreateCommand(commandName);
            command.Execute();
            switch (commandName)
            {
                case "show":
                    Show();
                    break;
                case "add":
                    Add(commandRest[1]);
                    break;
                case "check":
                    Check(commandRest[1]);
                    break;
                case "uncheck":
                    Uncheck(commandRest[1]);
                    break;
                case "help":
                    Help();
                    break;
                default:
                    Error(commandName);
                    break;
            }
        }

        private void Show()
        {
            foreach (var project in _tasks)
            {
                _console.WriteLine(project.Key);
                foreach (Task task in project.Value)
                {
                    _console.WriteLine("    [{0}] {1}: {2}", (task.Done ? 'x' : ' '), task.Id, task.Description);
                }
                _console.WriteLine();
            }
        }

        private void Add(string commandLine)
        {
            string[] subcommandRest = commandLine.Split(" ".ToCharArray(), 2);
            string subcommand = subcommandRest[0];
            if (subcommand == "project")
            {
                AddProject(subcommandRest[1]);
            }
            else if (subcommand == "task")
            {
                string[] projectTask = subcommandRest[1].Split(" ".ToCharArray(), 2);
                AddTask(projectTask[0], projectTask[1]);
            }
        }

        private void AddProject(string name)
        {
            _tasks[name] = new List<Task>();
        }

        private void AddTask(string project, string description)
        {
            if (!_tasks.ContainsKey(project))
            {
                _console.WriteLine("Could not find a project with the name \"{0}\".", project);
                return;
            }
            _tasks[project].Add(new Task { Id = NextId(), Description = description, Done = false });
        }

        private void Check(string idString)
        {
            SetDone(idString, true);
        }

        private void Uncheck(string idString)
        {
            SetDone(idString, false);
        }

        private void SetDone(string idString, bool done)
        {
            int id = int.Parse(idString);
            Task identifiedTask = _tasks
                .Select(project => project.Value.FirstOrDefault(task => task.Id == id))
                .Where(task => task != null)
                .FirstOrDefault();
            if (identifiedTask == null)
            {
                _console.WriteLine("Could not find a task with an ID of {0}.", id);
                return;
            }

            identifiedTask.Done = done;
        }

        private void Help()
        {
            _console.WriteLine("Commands:");
            _console.WriteLine("  show");
            _console.WriteLine("  add project <project name>");
            _console.WriteLine("  add task <project name> <task description>");
            _console.WriteLine("  check <task ID>");
            _console.WriteLine("  uncheck <task ID>");
            _console.WriteLine();
        }

        private void Error(string command)
        {
            _console.WriteLine("I don't know what the command \"{0}\" is.", command);
        }

        private long NextId()
        {
            return ++_lastId;
        }
    }
}