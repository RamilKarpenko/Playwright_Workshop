namespace PlaywrightWorkshop;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{

    public override BrowserNewContextOptions ContextOptions()
    {

        return new BrowserNewContextOptions()
        {
            ColorScheme = ColorScheme.Light,
            ViewportSize = new()
            {
                Width = 1920,
                Height = 1080
            },
            //BaseURL = baseUrl,
        };
    }

    [Test]
    public async Task SetupAndDocs()
    {
        //https://playwright.dev/dotnet/docs/intro
        //https://playwright.dev/dotnet/docs/library
    }

    [Test]
    public async Task Pipeline()
    {
        //https://playwright.dev/dotnet/docs/ci
    }

}