using NUnit.Framework;

namespace TaskListKata.Tests
{
    [TestFixture]
    public class TaskRepositoryShould
    {
        private TaskRepository _taskRepository;

        [SetUp]
        public void Init()
        {
            _taskRepository = new TaskRepository();
        }

        [Test]
        public void store_a_task()
        {
            var project = new Project("secrets");
            var task = new Task("Eat more donuts.", project);
            
            _taskRepository.Add(task);

            Assert.That(_taskRepository.Tasks()[0], Is.EqualTo(task));
        }

        [Test]
        public void find_a_task_by_an_id()
        {
            var project = new Project("secrets");
            var task = new Task("Eat more donuts.", project);

            _taskRepository.Add(task);

            Assert.That(_taskRepository.FindTaskById(task.Id.ToString()), Is.EqualTo(task));
        }

        [Test]
        public void create_a_task_list_by_projects()
        {
            var project = new Project("secrets");
            var task = new Task("Eat more donuts.", project);
            _taskRepository.Add(task);

            var projectWithTasks = _taskRepository.TasksByProjects();

            Assert.That(projectWithTasks.ContainsKey(project), Is.True);
            Assert.That(projectWithTasks[project].Contains(task), Is.True);
        }

        [Test]
        public void check_if_is_null_or_empty()
        {
            Assert.That(_taskRepository.IsNullOrEmpty(), Is.True);
        }

    }
}