using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            app.Navigator.GoToNewContactPage();
            ContactData contact = new ContactData("Test", "Testov");
            app.Contacts
                .FillContactCreationForm(contact)
                .SubmitContactCreationForm();
            app.Auth.Logout();
        }
    }
}
