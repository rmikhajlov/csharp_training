using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTest : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            ContactData contactFromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData contactFromEditForm = app.Contacts.GetContactInformationFromEditForm(0);

            // verification
            Assert.AreEqual(contactFromTable, contactFromEditForm);
            Assert.AreEqual(contactFromTable.Address, contactFromEditForm.Address);
            Assert.AreEqual(contactFromTable.AllPhones, contactFromEditForm.AllPhones);
            Assert.AreEqual(contactFromTable.AllEmails, contactFromEditForm.AllEmails);
        }
    }
}
