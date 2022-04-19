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
            ProjectData project = new ProjectData("project_12");
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            List<ProjectData> oldProjects = app.Api.GetProjectList(account);
            app.Projects.Create(project);

            List<ProjectData> newProjects = app.Api.GetProjectList(account);

            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
