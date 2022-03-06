using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            int index = 1;
            ContactData newContact = new ContactData("new-Name", "new-Lastname");

            if (app.Contacts.IsContactTableEmpty())
            {
                ContactData contact = new ContactData("Name", "Lastname");
                app.Contacts.Create(contact);
                app.Contacts.Modify(1, newContact);
                return;
            }

            app.Contacts.Modify(index, newContact);
        }
    }
}
