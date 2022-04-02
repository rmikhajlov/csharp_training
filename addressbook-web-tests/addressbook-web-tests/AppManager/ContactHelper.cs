using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {

        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToNewContactPage();
            FillContactCreationForm(contact);
            SubmitContactCreationForm();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper Modify(int index, ContactData newContact)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);
            FillContactCreationForm(newContact);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Modify(ContactData contact, ContactData newContact)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(contact.Id);
            FillContactCreationForm(newContact);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int index)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(index);
            InitContactRemoval();
            ConfirmContactRemoval();
            driver.FindElement(By.CssSelector("div.msgbox"));
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper Remove(ContactData contact)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(contact.Id);
            InitContactRemoval();
            ConfirmContactRemoval();
            driver.FindElement(By.CssSelector("div.msgbox"));
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper ConfirmContactRemoval()
        {
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactRemoval()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            index = index + 2;
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + index + "]/td/input")).Click();
            return this;
        }

        public ContactHelper SelectContact(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SubmitContactCreationForm()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            index = index + 2;
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + index + "]/td[8]")).Click();
            return this;
        }

        public ContactHelper InitContactModification(string contactId)
        {
            driver.FindElement(By.XPath("//a[@href='edit.php?id=" + contactId + "']")).Click();
            return this;
        }

        public ContactHelper FillContactCreationForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("lastname"), contact.LastName);
            return this;
        }

        public bool IsContactTableEmpty()
        {
            manager.Navigator.GoToHomePage();
            return !IsElementPresent(By.Name("selected[]"));
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.XPath("//tr[@name='entry']"));
                foreach (IWebElement element in elements)
                {
                    string firstName = element.FindElement(By.XPath("./td[3]")).Text;
                    string lastName = element.FindElement(By.XPath("./td[2]")).Text;

                    contactCache.Add(new ContactData(firstName, lastName)
                    {
                        Id = element.FindElement(By.Name("selected[]")).GetAttribute("value")
                    });
                }
            }

            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.XPath("//tr[@name='entry']")).Count;
        }

        public int GetDisplayedContactCount()
        {
            return driver.FindElements(By.XPath("//tr[@name='entry' and string-length(@style)=0]")).Count;
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells =  driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));

            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allPhones = cells[5].Text;
            
            IList<IWebElement> emails = cells[4].FindElements(By.TagName("a"));
            string allEmails = "";
            foreach (IWebElement email in emails)
            {
                allEmails += email.Text;
            }



            ContactData contact = new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };

            return contact;
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string firstEmail = driver.FindElement(By.Name("email")).GetAttribute("value");
            string secondEmail = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string thirdEmail = driver.FindElement(By.Name("email3")).GetAttribute("value");

            ContactData contact = new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                FirstEmail = firstEmail,
                SecondEmail = secondEmail,
                ThirdEmail = thirdEmail
            };

            return contact;
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return int.Parse(m.Value);
        }

        public void FillSearchString(string value)
        {
            Type(By.Name("searchstring"), value);
        }

        public ContactData GetContactInformationFromDetails(int index)
        {
            manager.Navigator.GoToHomePage();
            GoToContactDetailsPage(index);
            // string allInfo = Regex.Replace(driver.FindElement(By.Id("content")).Text, "[\r\n]", "");
            string allInfo = driver.FindElement(By.Id("content")).Text;

            ContactData contact = new ContactData()
            {
                AllInfo = allInfo
            };

            return contact;
        }

        public void GoToContactDetailsPage(int index)
        {
            index++;
            driver.FindElement(By.XPath("//tr[@name='entry'][" + index + "]/td[7]")).Click();
        }

        public ContactHelper AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            return this;
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public ContactHelper RemoveContactFromGroup(ContactData contactToRemove, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            SelectGroup(group.Name);
            SelectContact(contactToRemove.Id);
            CommitRemovingContactFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            return this;
        }

        public void CommitRemovingContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        public void SelectGroup(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }
    }
}
