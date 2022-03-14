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
            int index = 0;
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

            List<GroupData> oldGroups = app.Groups.GetGroupList();
            GroupData oldData = oldGroups[index];

            app.Groups.Modify(index, newGroup);

            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[index].Name = newGroup.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(group.Name, newGroup.Name);
                }
            }
        }
    }
}
