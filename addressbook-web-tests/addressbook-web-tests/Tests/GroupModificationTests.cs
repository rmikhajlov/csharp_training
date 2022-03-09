using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            int index = 1;
            GroupData newGroup = new GroupData("new name");
            newGroup.Header = "new header";
            newGroup.Footer = "new footer";

            if (app.Groups.IsGroupTableEmpty())
            {
                GroupData group = new GroupData("name");
                group.Header = "header";
                group.Footer = "footer";
                app.Groups.Create(group);
            }
            app.Groups.Modify(index, newGroup);
        }
    }
}
