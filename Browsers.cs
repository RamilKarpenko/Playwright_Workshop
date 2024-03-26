namespace PlaywrightWorkshop
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Browsers : PlaywrightTest
    {
        // chromium | firefox | webkit | msegde | chrome
        [Test]
        public async Task ChooseBrowser()
        {
            // by config .runsettings
            // -- Playwright.BrowserName=webkit

            var browser = Playwright.Firefox.LaunchAsync();

        }
    }
}
