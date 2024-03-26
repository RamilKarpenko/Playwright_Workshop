First, copy the repository, and move to it
<p></p>
Then build a project: `dotnet build` 
<p></p>
Install the <a href="https://playwright.dev/dotnet/docs/browsers">browsers</a>: `pwsh bin/Debug/netX/playwright.ps1 install --with-deps`

Run the tests using the dotnet test command, and also take a look at <a href="https://playwright.dev/dotnet/docs/running-tests">this page</a> for more information 
<p></p>
To use the `.runsettings` configuration file, specify its file with the `--settings:example.runsettings flag`