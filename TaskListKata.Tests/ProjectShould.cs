using NUnit.Framework;

namespace TaskListKata.Tests
{
    [TestFixture]
    public class ProjectShould
    {
        [SetUp]
        public void Init()
        {

        }

        [Test]
        public void ReturnItsName()
        {
            var project = new Project("secrets");

            Assert.That(project.Name(), Is.EqualTo("secrets"));
        }



    }
}