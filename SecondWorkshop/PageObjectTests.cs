using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightWorkshop.SecondWorkshop
{
    public class PageObjectTests : PageTest
    {
        [Test]
        public async Task SimpleTest()
        {
            var page = new SearchPage(Page);
            await page.GotoAsync();
            await page.SearchAsync("search query");
        }
    }
}
