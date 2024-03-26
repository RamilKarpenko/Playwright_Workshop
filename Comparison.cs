namespace PlaywrightWorkshop
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class ComparisonTest : PageTest
    {
        // async, return type = Task 
        [SetUp]
        public async Task NoNeedToCreateDriverInstance()
        {
            // you don't need to initialize driver instance
            await Page.GotoAsync("https://the-internet.herokuapp.com/");

            var browser = await Playwright.Chromium.LaunchAsync();
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            //-- NUnit.NumberOfTestWorkers=5
        }

        //https://playwright.dev/dotnet/docs/locators
        [Test]
        public async Task LocatorsStrategies()
        {
            ILocator checkboxesLink = Page.GetByRole(AriaRole.Link, new() { Name = "Checkboxes" });
            ILocator pageHeader = Page.Locator(".heading");

            await Page.GotoAsync("https://the-internet.herokuapp.com/");
            
            ILocator addRemoveLink = Page.Locator("//a[@href='/add_remove_elements/']");
            ILocator dropdownLink = Page.GetByText("Dropdown");
            Console.WriteLine(await pageHeader.TextContentAsync());
            Console.WriteLine(await checkboxesLink.TextContentAsync());
            Console.WriteLine(await addRemoveLink.TextContentAsync());
            Console.WriteLine(await dropdownLink.TextContentAsync());
        }

        //https://playwright.dev/dotnet/docs/actionability
        [Test]
        public async Task AutoWaits()
        {
            await Page.GotoAsync("https://the-internet.herokuapp.com/");
            ILocator startButton = Page.GetByRole(AriaRole.Button, new() { Name = "Start" });
            ILocator text = Page.Locator("#finish h4");
            await startButton.ClickAsync();
            // Autowait
            // timeout = 30s (default)
            string elementText = await text.TextContentAsync();
            Console.WriteLine(elementText);

            await Page.ReloadAsync();
            await startButton.ClickAsync();
            // timeout = 5s (default), could be changed in .runsettings or passed to expect method
            await Expect(text).ToBeVisibleAsync();
        }

        [Test]
        public async Task AlertHandler()
        {
            //automatic alert handler
            await Page.GotoAsync("https://the-internet.herokuapp.com/javascript_alerts");

            ILocator triggerAlertButton = Page.Locator("[onclick='jsAlert()']");
            ILocator triggerConfirmButton = Page.Locator("[onclick='jsConfirm()']");
            ILocator triggerPromptButton = Page.Locator("[onclick='jsPrompt()']");

            await triggerAlertButton.ClickAsync();
            await Page.WaitForTimeoutAsync(2000);
            await triggerConfirmButton.ClickAsync();
            await Page.WaitForTimeoutAsync(2000);
            await triggerPromptButton.ClickAsync();

            //specify your own handler
            Page.Dialog += async (_, dialog) =>
            {
                Console.WriteLine(dialog.Message);
                await Page.WaitForTimeoutAsync(2000);

                if (dialog.Type == "prompt")
                {
                    await dialog.AcceptAsync("may force be with you");
                    await Page.WaitForTimeoutAsync(2000);
                }
                await dialog.DismissAsync();
            };

            await triggerAlertButton.ClickAsync();
            await triggerConfirmButton.ClickAsync();
            await triggerPromptButton.ClickAsync();
        }

    }
}
