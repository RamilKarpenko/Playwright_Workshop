First, copy the repo
    `git clone https://github.com/RamilKarpenko/Playwright_Workshop.git`

Move to the repo:
  `cd Playwright_Workshop`

Build a project:
 `dotnet build`
 Install <a href="https://playwright.dev/dotnet/docs/browsers">browsers</a>:
 `pwsh bin/Debug/netX/playwright.ps1 install --with-deps`

Run tests using command `dotnet test`, and take a look on <a href="https://playwright.dev/dotnet/docs/running-tests">this page</a>
To use .runsettings config file, specify its file using `--settings:example.runsettings` flag
