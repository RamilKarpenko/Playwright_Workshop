namespace PlaywrightWorkshop.FirstWorkshop
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Waits : PageTest
    {
        //https://playwright.dev/dotnet/docs/api/class-page#page-wait-for-request

        [SetUp]
        public async Task GoToUrl()
        {
            await Page.GotoAsync("https://testpages.eviltester.com/");
            // wait for specific url
            await Page.WaitForURLAsync("**/styled/*");
        }

        //https://playwright.dev/dotnet/docs/api/class-locator#locator-wait-for
        [Test]
        public async Task WaitForLocator()
        {
            ILocator messagesPageLink = Page.Locator("#xhttpmessages");

            // wait for specific element to be visible (default), timeout = 30s (default)
            await messagesPageLink.WaitForAsync(new() { State = WaitForSelectorState.Detached });

            await messagesPageLink.ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.Load);
        }

        [Test]
        public async Task NetworkWaits()
        {

            // do something and wait for request
            await Page.RunAndWaitForResponseAsync(async () =>
            {
                await Page.ReloadAsync();

            }, "https://testpages.eviltester.com/styled/sync/messageset02.json");

            await Page.WaitForResponseAsync("https://testpages.eviltester.com/styled/sync/messageset01.json");

            var wait = Page.WaitForRequestAsync("https://testpages.eviltester.com/styled/sync/messageset02.json");

            //wait by predicate
            var request = await Page.WaitForRequestAsync((request) =>
            { return request.Url == "https://testpages.eviltester.com/styled/sync/messageset01.json" && request.Method == "GET"; });

            Console.WriteLine(request.Url);

            await Page.WaitForRequestFinishedAsync(new()
            {
                Predicate =
                request => request.Url == "https://testpages.eviltester.com/styled/sync/messageset02.json"
            });




        }

        [Test]
        public async Task ConsoleWait()
        {
            //create waiter, specify conditions
            Task<IConsoleMessage> messageWait = Page.WaitForConsoleMessageAsync(new() { Predicate = (message) => message.Args.Count == 2 });
            // trigger event
            await Page.EvaluateAsync("console.log('General', 'Kenobi')");
            // wait for event, and store results
            IConsoleMessage message = await messageWait;
            Console.WriteLine("Hello, there!\n{0} {1}!", await message.Args[0].JsonValueAsync<string>(), await message.Args[1].JsonValueAsync<string>());
        }

        [Test]
        public async Task PopupsWaits()
        {
            await Page.RunAndWaitForPopupAsync(async () =>
            {
                await Page.EvaluateAsync("window.open('https://javascript.info');");
            }, new() { Predicate = page => page.Url.Contains("javascript") });
        }
    }
}
