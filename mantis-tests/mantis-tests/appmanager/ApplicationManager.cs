using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests

{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;
        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0.2);
            baseURL = "http://localhost/mantisbt";
            Registration = new RegistrationHelper(this);
            Ftp = new FtpHelper(this);
            James = new JamesHelper(this);
            Mail = new MailHelper(this);
            Auth = new LoginHelper(this);
            Projects = new ProjectHelper(this);
            Navigator = new NavigationHelper(this, baseURL);
            Admin = new AdminHelper(this, baseURL);
        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = newInstance.baseURL + "/login_page.php";
                app.Value = newInstance;
            }

            return app.Value;
        }

        public IWebDriver Driver {
            get
            {
                return driver;
            }
        }

        public RegistrationHelper Registration { get; }
        public FtpHelper Ftp { get; }

        public JamesHelper James { get; }
        public MailHelper Mail { get; }
        public LoginHelper Auth { get; }
        public ProjectHelper Projects { get; }
        public NavigationHelper Navigator { get; }
        public AdminHelper Admin { get; }
    }
}
