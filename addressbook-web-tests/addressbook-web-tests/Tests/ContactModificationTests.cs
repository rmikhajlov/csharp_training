using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            int index = 0;
            ContactData newContact = new ContactData("new-Name", "new-Lastname");

            if (app.Contacts.IsContactTableEmpty())
            {
                ContactData contact = new ContactData("Name", "Lastname");
                app.Contacts.Create(contact);
            }

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData oldData = oldContacts[index];

            app.Contacts.Modify(oldData, newContact);

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts[index].FirstName = newContact.FirstName;
            oldContacts[index].LastName = newContact.LastName;
            
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == oldData.Id)
                {
                    Assert.AreEqual(newContact.FirstName, contact.FirstName);
                }
            }
        }
    }
}
