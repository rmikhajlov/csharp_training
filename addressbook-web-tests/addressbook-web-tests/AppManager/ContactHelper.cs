﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

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
    }
}
