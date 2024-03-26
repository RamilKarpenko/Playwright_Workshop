First, copy the repository, and move to it
<p></p>
Then build a project: <code>dotnet build`</code> 
<p></p>
Install the <a href="https://playwright.dev/dotnet/docs/browsers">browsers</a>: <code>pwsh bin/Debug/netX/playwright.ps1 install --with-deps</code>

Run the tests using the <code>dotnet test</code> command, and also take a look at <a href="https://playwright.dev/dotnet/docs/running-tests">this page</a> for more information 
<p></p>
To use the <code>.runsettings</code> configuration file, specify its file with the <code>--settings:example.runsettings flag</code>