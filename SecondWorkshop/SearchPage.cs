namespace PlaywrightWorkshop.SecondWorkshop
{
    public class SearchPage
    {
        private readonly IPage _page;
        private readonly ILocator _searchTermInput; // => _page.Locator("[aria-label='Enter your search term']")

        //public SearchPage(IPage page) => _page = page;

        public SearchPage(IPage page)
        {
            _page = page;
            _searchTermInput = page.Locator("[aria-label='Enter your search term']");
        }

        public async Task GotoAsync()
        {
            await _page.GotoAsync("https://bing.com");
        }

        public async Task SearchAsync(string text)
        {
            await _searchTermInput.FillAsync(text);
            await _searchTermInput.PressAsync("Enter");
        }

        public async Task CheckTextInSearchInput(string text)
        {
            await Assertions.Expect(_searchTermInput).ToBeVisibleAsync();
        }
    }
}
