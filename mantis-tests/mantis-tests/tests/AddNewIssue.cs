using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class AddNewIssue : TestBase
    {
        [Test]
        public void TestAddNewIssue()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            IssueData issueData = new IssueData()
            {
                Summary = "some short text",
                Description = "some long text",
                Category = "General"
            };

            ProjectData project = new ProjectData("test")
            {
                Id = "19"
            };

            app.Api.CreateNewIssue(account, issueData, project);
        }
    }
}
