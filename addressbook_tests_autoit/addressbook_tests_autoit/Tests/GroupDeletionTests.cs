using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_tests_autoit
{
    [TestFixture]
    public class GroupDeletionTests : TestBase
    {
        [Test]
        public void TestGroupDeletion()
        {
            // Переменная для хранения индекса удаляемой группы
            int index = 0;

            // Запросить список групп, сохранить в переменную
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            // Если есть только 1 группа, нужно создать ещё одну, т.к. удалять единственную группу нельзя
            // И обновить список групп
            if (oldGroups.Count == 1)
            {
                app.Groups.Add(new GroupData()
                {
                    Name = "new group name"
                });
                oldGroups = app.Groups.GetGroupList();
            }

            // Удалить группу по заданному индексу
            app.Groups.Delete(index);

            // Снова запросить список групп, сохранить в новую переменную
            List<GroupData> newGroups = app.Groups.GetGroupList();

            // Удалить из старого списка заданный элемент
            oldGroups.RemoveAt(index);

            // Отсортировать старый и новый списки
            oldGroups.Sort();
            newGroups.Sort();

            // Сравнить старый и новый списки
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
