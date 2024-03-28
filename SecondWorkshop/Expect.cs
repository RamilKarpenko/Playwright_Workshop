namespace PlaywrightWorkshop.SecondWorkshop
{
    //https://playwright.dev/dotnet/docs/test-assertions
    [TestFixture]
    public class Expect : PageTest
    {
        //https://playwright.dev/dotnet/docs/api/class-locatorassertions#locator-assertions-to-be-attached
        [Test]
        public async Task LocatorAssertions()
        {
            SetDefaultExpectTimeout(10_000);

            await Page.GotoAsync("https://the-internet.herokuapp.com/dynamic_loading/2");
            await Page.Locator("#start button").ClickAsync();
            ILocator text = Page.Locator("#finish h4");
            await Expect(text).ToHaveCSSAsync("display", "block");
            await Expect(text).Not.ToHaveTextAsync("Wrong text");
        }

        //https://playwright.dev/dotnet/docs/api/class-pageassertions
        [Test]
        public async Task PageAssertions()
        {
            string url = "https://the-internet.herokuapp.com/dynamic_loading/2";
            await Page.GotoAsync(url);
            await Expect(Page).ToHaveURLAsync(url);
            await Expect(Page).Not.ToHaveTitleAsync("Playwright");
        }

        //https://playwright.dev/dotnet/docs/api/class-apiresponseassertions
        [Test]
        public async Task APIAssertions()
        {
            var apiContext = await Playwright.APIRequest.NewContextAsync();
            var response = await apiContext.GetAsync("https://api.github.com");
            await Expect(response).ToBeOKAsync();
        }
    }
}
