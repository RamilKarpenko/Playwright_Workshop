using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightWorkshop.SecondWorkshop
{
    public class Locators : BrowserTest
    {
        IPage page;
        [SetUp]
        public async Task InitializePage()
        {
            var context = await Browser.NewContextAsync(
                new BrowserNewContextOptions() { BaseURL = "https://the-internet.herokuapp.com"});
            page = await context.NewPageAsync();
            await page.GotoAsync("/challenging_dom");
        }

        [Test]
        public async Task LocatorTypes()
        {
            // by aria-role
            //https://www.w3.org/TR/wai-aria-1.2/#roles
            //https://www.w3.org/TR/wai-aria-1.2/#aria-attributes
            //https://w3c.github.io/accname/#dfn-accessible-name

            var table = page.GetByRole(AriaRole.Table);
            //locator chain
            var tableButtons = table.GetByRole(AriaRole.Link);
            //tableButtons is a list of elements
            await Expect(tableButtons).ToHaveCountAsync(20);
            var list = Enumerable.Repeat(new[] { "edit", "delete" }, 10).SelectMany(x => x).ToList();

            await Expect(tableButtons).ToHaveTextAsync(list);

            var specificTableCell = table.GetByRole(AriaRole.Cell, new() { Name = "Iuvaret0" });
            await Expect(specificTableCell).ToBeVisibleAsync();

            //by text
            var anotherTableCell = page.GetByText("Definiebas5");
            await Expect(anotherTableCell).ToBeVisibleAsync();

            await page.GotoAsync("/iframe");
            var boldButton = page.GetByTitle("Bold");
            await Expect(boldButton).ToBeVisibleAsync();
            await boldButton.ClickAsync();

            //aria attribute
            var pressedButton = page.GetByRole(AriaRole.Button, new() { Pressed = true });
            await Expect(boldButton).ToHaveAttributeAsync("aria-pressed", "true");

            //testid attr
            Playwright.Selectors.SetTestIdAttribute("aria-label");
            var italicButton = page.GetByTestId("Italic");
            await Expect(italicButton).ToBeVisibleAsync();

            IFrameLocator frame = page.FrameLocator("[title='Rich Text Area']");
            var input = frame.Locator("p");
            await input.FillAsync("Whole new text!");
            IFrame sameFrame = page.FrameByUrl("https://the-internet.herokuapp.com/iframe");
            var sameInput = sameFrame.Locator("p");
            await Expect(sameInput).ToHaveTextAsync(await input.TextContentAsync());
        }

        [Test]
        public async Task LocatorFilters()
        {
            var cells = page.GetByRole(AriaRole.Cell);
            string cellText = "Phaedrum4";
            var specificCell = cells.Filter(new() { HasText = cellText });
            await Expect(specificCell).ToBeVisibleAsync();
            await Expect(specificCell).ToHaveTextAsync(cellText);

            var divs = page.Locator("#content .columns");
            var divWithButton = divs.Filter(new() { HasNot = page.GetByRole(AriaRole.Table).Or(page.Locator("canvas")) });
            await Expect(divWithButton).ToHaveCountAsync(1);
            var alertButton = divWithButton.GetByRole(AriaRole.Link).And(divWithButton.Locator(".alert"));
            await Expect(alertButton).ToHaveCountAsync(1);
            await Expect(alertButton).ToBeVisibleAsync();

        }
    }
}
