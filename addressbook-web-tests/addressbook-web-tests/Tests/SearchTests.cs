using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class SearchTests : AuthTestBase
    {
        [Test]
        public void TestSearch()
        {
            // Т.к. если элемент со страницы не подходит под поиск, со страницы он никуда не пропадает,
            // а ему просто проставляется display: none, старый метод GetContactCount() не подходил - он 
            // считал и скрытые элементы. Пришлось написать отдельный новый метод GetDisplayedContactCount().

            string searchString = "w";
            // Сначала сравниваем до ввода символа в строку
            Assert.AreEqual(app.Contacts.GetNumberOfSearchResults(), app.Contacts.GetDisplayedContactCount());

            // Вводим символ в строку
            app.Contacts.FillSearchString(searchString);

            // И сравниваем после ввода символа в строку
            Assert.AreEqual(app.Contacts.GetNumberOfSearchResults(), app.Contacts.GetDisplayedContactCount());
        }
    }
}
