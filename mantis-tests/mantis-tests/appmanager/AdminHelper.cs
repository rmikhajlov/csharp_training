using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SimpleBrowser.WebDriver;
using System.Text.RegularExpressions;

namespace mantis_tests
{
    public class AdminHelper: HelperBase
    {
        private string baseURL;

        public AdminHelper(ApplicationManager manager, string baseURL) : base(manager) {
            this.baseURL = baseURL;
        }

        public List<AccountData> GetAllAccounts()
        {
            List<AccountData> accounts = new List<AccountData>();
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseURL + "/manage_user_page.php";
            ICollection<IWebElement> elements = driver.FindElements(By.XPath("//div[@id='main-container']/div[2]/div[2]/div/div/div[4]/div[2]/div[2]/div/table/tbody/tr"));
            foreach (IWebElement element in elements)
            {
                IWebElement link = element.FindElement(By.XPath("./td/a"));
                string name = link.Text;
                string href = link.GetAttribute("href");
                Match match = Regex.Match(href, @"\d+$");
                accounts.Add(new AccountData(name) {
                    Id = match.Value
                });
            }
            return accounts;
        }


        public void DeleteAccount(AccountData account)
        {
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseURL + "/manage_user_edit_page.php?user_id=" + account.Id;
            driver.FindElement(By.XPath("//input[@value='Delete User']")).Click();
            driver.FindElement(By.XPath("//input[@value='Delete Account']")).Click();
        }

        public IWebDriver OpenAppAndLogin()
        {
            IWebDriver driver = new SimpleBrowserDriver();
            driver.Url = baseURL + "/login_page.php";
            driver.FindElement(By.Id("username")).SendKeys("administrator");
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
            driver.FindElement(By.Id("password")).SendKeys("root");
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
            return driver;
        }
    }
}
