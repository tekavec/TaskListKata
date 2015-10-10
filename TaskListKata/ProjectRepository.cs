using System.Collections.Generic;
using System.Linq;

namespace TaskListKata
{
    public class ProjectRepository : IProjectRepository
    {
        private IList<Project> _projects;

        public void Add(Project project)
        {
            if (_projects == null)
            {
                _projects = new List<Project>();
            }
            _projects.Add(project);
        }

        public IList<Project> AllProjects()
        {
            return _projects;
        }

        public bool IsNullOrEmpty()
        {
            return _projects == null || _projects.Count == 0;
        }

        public Project FindProjectByName(string name)
        {
            return _projects.SingleOrDefault(a => a.Name() == name);
        }
    }
}