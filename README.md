# Scarlet-Sideloader
A cydia impactor style tool to quickly patch and push apps to retail 

The code is extremly messy. The project is also based purely on reverse engineering using burpsuite, no documentation or proprietary information were used in the creation of this and it shows.
```
Usage:
  Scarlett-Sideloader [options] <cookie> <file>

Arguments:
  <cookie>    Your asp.net.cookies
  <file>      The path to your appx, msix, appxbundle and msixbundle

Options:
  -N, -n, --name <name>        Name to use for the app store page (if left blank it will be randomly generated).
  -A, -a, --app                Install as an app rather than a game (defaults to game).
  -E, -e, --emails <emails>    Emails to whitelist, seperated by commas.
  -G, -g, --groups <groups>    Groups to whitelist, seperated by commas.
  -O, -o, --original           Keep package file as original.
  --version                    Show version information
  -?, -h, --help               Show help and usage information
```
example:
`dotnet Scarlett-Sideloader.dll -G testgroup -E test@domain.com <cookie (.AspNet.Cookies)> test.appxbundle`

You can also use partner token to help speed up pulling the needed cookie from partner center.



Ps: This will be my last release for a while
