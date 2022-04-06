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
            GroupData group;
            ContactData contact;

            // Если ни одной группы не создано, создаём.
            if (GroupData.GetAll().Count == 0)
            {
                app.Groups.Create(new GroupData("new group name", "new group header", "new group footer"));
            }

            group = GroupData.GetAll()[0];

            // Если ни одного контакта не создано, создаём.
            if (ContactData.GetAll().Count == 0)
            {
                contact = new ContactData("new contact name", "new contact lastname");
                app.Contacts.Create(contact);
            }

            // Если в группе ни одного контакта, добавляем существующий контакт в группу.
            if (!app.Groups.IsAnyContactsInGroup(group))
            {
                contact = ContactData.GetAll()[0];
                app.Contacts.AddContactToGroup(contact, group);
            }

            List<ContactData> oldList = group.GetContacts();

            contact = oldList[0];
            app.Contacts.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            newList.Sort();
            oldList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
