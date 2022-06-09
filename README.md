# Scarlet-Sideloader
A cydia impactor style tool to quickly patch and push apps to retail 

The code is extremely messy. The project is also based purely on reverse engineering using burpsuite, no documentation or proprietary information were used in the creation of this and it shows.

It is reccomended that you manually compiled .NET apps though you can technically utilise the `--original` option.
```
Usage:
  Scarlett-Sideloader [options] <cookie> <file>

Arguments:
  <cookie>    Your asp.net.cookies
  <file>      The path to your appx, msix, appxbundle and msixbundle

Options:
  -N, -n, --name <name>                  Name to use for the app store page (if left blank it will be randomly
                                         generated).
  -D, -d, --description <description>    Description to display on store page. [default: a really cool uwp app]
  -S, -s, --screenshot <screenshot>      Image to use for screenshot on storepage. [default: blank.png]
  -A, -a, --app                          Install as an app rather than a game (defaults to game).
  -P, -p, --public                       Push as public instead of defaulting to a private app [default: False]
  -E, -e, --emails <emails>              Emails to whitelist, seperated by commas.
  -G, -g, --groups <groups>              Group names to whitelist, seperated by commas.
  -O, -o, --original                     Keep package file as original.
  --version                              Show version information
  -?, -h, --help                         Show help and usage information
```
## Example
`dotnet Scarlett-Sideloader.dll -G testgroup -E test@domain.com <cookie (.AspNet.Cookies)> test.appxbundle`
You can also use partner token to help speed up pulling the needed cookie from partner center.

## Config
You will need to create a folder in the same dir as the exe called appx packer and add makemsix to that folder. If you have a folder set up for appxpacker already you can just drop in that same folder and it should work.

Ps: This will be my last release for a while
