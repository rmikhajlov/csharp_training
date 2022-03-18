using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
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


        [Test]
        public void TestContactDetailsInformation()
        {
            int index = 0;

            ContactData contactFromDetails = app.Contacts.GetContactInformationFromDetails(index);
            ContactData contactFromEditForm = app.Contacts.GetContactInformationFromEditForm(index);

            Assert.AreEqual(contactFromDetails.AllInfo, contactFromEditForm.AllInfo);
        }
    }
}
