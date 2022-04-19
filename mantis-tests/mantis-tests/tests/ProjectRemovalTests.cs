using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemovalTests: AuthTestBase
    {
        [Test]
        public void TestProjectRemoval()
        {
            int index = 0;

            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            if (app.Api.GetProjectList(account).Count == 0)
            {
                ProjectData project = new ProjectData("new_project");
                app.Api.AddProject(account, project);
            }

            List<ProjectData> oldProjects = app.Projects.GetAll();
            ProjectData toBeRemoved = oldProjects[index];
            app.Projects.Remove(toBeRemoved);

            List<ProjectData> newProjects = app.Projects.GetAll();
            oldProjects.RemoveAt(index);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);

            foreach (ProjectData p in newProjects)
            {
                Assert.AreNotEqual(p.Name, toBeRemoved.Name);
            }
        }
    }
}
