using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightWorkshop.SecondWorkshop
{
    [TestFixture]
    public class Configuration : BrowserTest
    {
        //https://playwright.dev/dotnet/docs/api/class-browsercontext#browser-context-set-default-navigation-timeout
        [Test]
        public async Task BrowserContextSetters()
        {
            //set context options
            var firstContext = await Browser.NewContextAsync(
                new() { BaseURL = "https://the-internet.herokuapp.com/dynamic_loading/2"});
            await Browser.NewPageAsync(new() { });
            await firstContext.NewPageAsync();//no configuration


            // set timeout for all things
            firstContext.SetDefaultTimeout(1_000);
            //set timeout only for navigation
            firstContext.SetDefaultNavigationTimeout(3_000);
            var firstContextPage = await firstContext.NewPageAsync();
            await firstContextPage.GotoAsync("");
            await firstContextPage.Locator("#start button").ClickAsync();
            ILocator text = firstContextPage.Locator("#finish h4");
            //timeout 1s
            string elementtext = await text.TextContentAsync();
            //timeout 5s
            await Expect(text).ToBeVisibleAsync();
        }

        //https://playwright.dev/dotnet/docs/api/class-page#page-set-default-navigation-timeout
        [Test]
        public async Task PageSetters()
        {
            var context = await Browser.NewContextAsync(new() { BaseURL = "https://the-internet.herokuapp.com/dynamic_loading/2" });
            context.SetDefaultNavigationTimeout(2_000);
            var page = await context.NewPageAsync();
            await page.SetViewportSizeAsync(720, 360);
            //rewrite context timout
            page.SetDefaultTimeout(1_000);
            page.SetDefaultNavigationTimeout(3_000);
            await page.GotoAsync("");
        }

        //https://playwright.dev/dotnet/docs/api/class-browser#events
        //https://playwright.dev/dotnet/docs/api/class-browsercontext#events
        [Test]
        public async Task BrowserAndBrowserContextEvents()
        {
            //
            Browser.Disconnected += async (_, second) =>
            {
                Console.WriteLine("Bye " +second.BrowserType.Name);
            };

            var context = await Browser.NewContextAsync();
            context.Close += (_, context) =>
            {
                Console.WriteLine("Page count = " +context.Pages.Count);
            };

            //close page, context and browser
            await Browser.CloseAsync();
        }

        [Test]
        public async Task PageEvents()
        {
            var page = await Browser.NewPageAsync();
            //when page closed
            page.Close += (_, page) =>
            {
                Console.WriteLine("Closed " + page.Url);
            };
           

            await page.GotoAsync("https://playwright.dev");

            page.FrameNavigated += (_, frame) =>
            {
                Console.WriteLine("Navigated to" + frame.Url);
            };

            await page.GotoAsync("https://playwright.dev/smth");
            //close only page
            await page.CloseAsync();
        }

    }
}
