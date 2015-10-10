using System.Collections.Generic;

namespace TaskListKata
{
    public class Project
    {
        private readonly string _name;
        private IList<Task> _taskRepository;

        public Project(string name)
        {
            _name = name;
        }

        public void Add(Task task)
        {
            if (_taskRepository == null)
            {
                _taskRepository = new List<Task>();
            }
            _taskRepository.Add(task);
        }

        public string Name()
        {
            return _name;
        }

        public IList<Task> Tasks()
        {
            return _taskRepository;
        }

        protected bool Equals(Project other)
        {
            return string.Equals(_name, other._name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Project) obj);
        }

        public override int GetHashCode()
        {
            return (_name != null ? _name.GetHashCode() : 0);
        }
    }
}