using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ProjectHelper: HelperBase
    {
        public ProjectHelper(ApplicationManager manager): base(manager) { }

        public List<ProjectData> GetAll()
        {
            manager.Navigator.GoToProjectsManagementPage();

            List<ProjectData> groupList = new List<ProjectData>();
            ICollection<IWebElement> elements = driver.FindElements(By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div[2]/div[2]/div/div[2]/table/tbody/tr"));
            foreach (IWebElement element in elements)
            {
                string name = element.FindElement(By.XPath("./td/a")).Text;
                groupList.Add(new ProjectData(name));
            }
            return groupList;
        }

        public void Create(ProjectData project)
        {
            manager.Navigator.GoToProjectsManagementPage();
            InitiateProjectCreation();
            FillProjectCreationForm(project);
            SubmitProjectCreation();
        }

        public void SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@value='Add Project']")).Click();
            driver.FindElement(By.LinkText("Proceed"));

        }

        public void FillProjectCreationForm(ProjectData project)
        {
            Type(By.Id("project-name"), project.Name);
        }

        public void InitiateProjectCreation()
        {
            driver.FindElement(By.XPath("//button[contains(text(),'Create New Project')]")).Click();
        }
    }
}
