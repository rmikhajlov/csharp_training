using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class NavigationHelper: HelperBase
    {
        private string baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager) {
            this.baseURL = baseURL;
        }

        public void GoToProjectsManagementPage()
        {
            if (driver.Url == baseURL + "/manage_proj_page.php")
            {
                return;
            }
            driver.Navigate().GoToUrl(baseURL + "/manage_proj_page.php");
        }
    }
}
