using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests: AuthTestBase
    {
        [Test]
        public void TestProjectCreation()
        {
            ProjectData project = new ProjectData("project_4");

            List<ProjectData> oldProjects = app.Projects.GetAll();
            app.Projects.Create(project);

            List<ProjectData> newProjects = app.Projects.GetAll();
            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();

            Console.WriteLine("Old: ");
            foreach (ProjectData p in oldProjects)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine("New: ");
            foreach (ProjectData p in newProjects)
            {
                Console.WriteLine(p);
            }

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
