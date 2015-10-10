using System.Collections.Generic;

namespace TaskListKata
{
    public interface IProjectRepository
    {
        void Add(Project project);
        IList<Project> AllProjects();
        bool IsNullOrEmpty();
        Project FindProjectByName(string name);
    }
}