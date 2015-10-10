using NUnit.Framework;

namespace TaskListKata.Tests
{
    [TestFixture]
    public class ProjectRepositoryShould
    {
        private ProjectRepository _projectRepository;
        private readonly Project _projectA = new Project("secrets");
        private readonly Project _projectB = new Project("training");

        [SetUp]
        public void Init()
        {
            _projectRepository = new ProjectRepository();
        }

        [Test]
        public void store_a_project()
        {
            _projectRepository.Add(_projectA);

            Assert.That(_projectRepository.AllProjects()[0], Is.EqualTo(_projectA));
        }

        [Test]
        public void ReturnAllProjects()
        {
            AddTwoProjectsToRepository();

            Assert.That(_projectRepository.AllProjects()[0], Is.EqualTo(_projectA));
            Assert.That(_projectRepository.AllProjects()[1], Is.EqualTo(_projectB));
        }

        [Test]
        public void CheckIfIsNullOrEmpty()
        {
            Assert.That(_projectRepository.IsNullOrEmpty(), Is.True);
        }

        [Test]
        public void FindAProjectByName()
        {
            _projectRepository.Add(_projectA);
            _projectRepository.Add(_projectB);

            Assert.That(_projectRepository.FindProjectByName(_projectA.Name()), Is.EqualTo(_projectA));
        }

        private void AddTwoProjectsToRepository()
        {
            _projectRepository.Add(_projectA);
            _projectRepository.Add(_projectB);
        }
         
    }
}