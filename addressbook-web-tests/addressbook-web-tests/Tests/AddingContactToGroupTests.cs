using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            GroupData group;
            ContactData contact;

            // Если ни одной группы не создано, создаём
            if (GroupData.GetAll().Count == 0)
            {
                app.Groups.Create(new GroupData("new group name", "new group header", "new group footer"));
            }

            group = GroupData.GetAll()[0];

            // Если ни одного контакта не создано, создаём 
            if (ContactData.GetAll().Count == 0)
            {
                contact = new ContactData("new contact name", "new contact lastname");
                app.Contacts.Create(contact);
            }

            // Получаем список контактов группы
            List<ContactData> oldList = group.GetContacts();

            // Если все контакты добавлены в группу, удаляем первый контакт из группы.
            // В дальнейшем используем именно этот удалённый контакт.
            // И обновляем значение oldList.
            if (ContactData.GetAll().Except(oldList).Count() == 0)
            {
                contact = group.GetContacts().First();
                app.Contacts.RemoveContactFromGroup(contact, group);
                oldList = group.GetContacts();
            // Иначе берём первый контакт из не добавленных в группу.
            }
            else
            {
                contact = ContactData.GetAll().Except(oldList).First();
            }

            // actions
            app.Contacts.AddContactToGroup(contact, group);
            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
