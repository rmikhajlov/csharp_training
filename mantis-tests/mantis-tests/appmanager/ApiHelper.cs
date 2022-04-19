using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class ApiHelper: HelperBase
    {
        public ApiHelper(ApplicationManager manager): base(manager) { }

        public void CreateNewIssue(AccountData account, IssueData issueData, ProjectData projectData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = projectData.Id;
            client.mc_issue_add(account.Name, account.Password, issue);
        }

        public List<ProjectData> GetProjectList(AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();

            List<ProjectData> projects = new List<ProjectData>();
            Mantis.ProjectData[] mantisProjects = client.mc_projects_get_user_accessible(account.Name, account.Password);

            foreach (Mantis.ProjectData p in mantisProjects)
            {
                projects.Add(new ProjectData(p.name) {
                    Id = p.id
                });
            }

            return projects;
        }

        public void AddProject(AccountData account, ProjectData project)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData mantisProject = new Mantis.ProjectData()
            {
                name = project.Name
            };

            client.mc_project_add(account.Name, account.Password, mantisProject);
        }
    }
}
