using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class RemovingContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void TestRemovingContactFromGroupTest()
        {
            GroupData group = GroupData.GetAll()[0];

            if (!app.Groups.IsAnyContactsInGroup(group))
            {
                ContactData first_contact = ContactData.GetAll()[0];
                app.Contacts.AddContactToGroup(first_contact, group);
                ContactData second_contact = ContactData.GetAll()[1];
                app.Contacts.AddContactToGroup(second_contact, group);
            }

            List<ContactData> oldList = group.GetContacts();

            ContactData contactToRemove = oldList[0];
            app.Contacts.RemoveContactFromGroup(contactToRemove, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contactToRemove);
            newList.Sort();
            oldList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
