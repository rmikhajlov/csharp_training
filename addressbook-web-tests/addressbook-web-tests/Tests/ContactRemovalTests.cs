using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            int index = 1;
            if (app.Contacts.IsContactTableEmpty())
            {
                ContactData contact = new ContactData("Name", "Lastname");
                app.Contacts.Create(contact);
                app.Contacts.Remove(1);
                return;
            }

            app.Contacts.Remove(index);
        }
    }
}
