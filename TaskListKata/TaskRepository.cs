using System.Collections.Generic;
using System.Linq;

namespace TaskListKata
{
    public class TaskRepository
    {
        private IList<Task> _tasks;

        public void Add(Task task)
        {
            if (_tasks == null)
            {
                _tasks = new List<Task>();
            }
            _tasks.Add(task);
        }

        public IList<Task> Tasks()
        {
            return _tasks;
        }

        public bool IsNullOrEmpty()
        {
            return _tasks == null || _tasks.Count == 0;
        }

        public IDictionary<Project, List<Task>> TasksByProjects()
        {
            return _tasks
                .GroupBy(x => x.Project())
                .ToDictionary(x => x.Key, x => x.ToList());
        }

        public Task FindTaskById(string id)
        {
            return _tasks.SingleOrDefault(a => a.Id.ToString() == id);
        }
    }
}