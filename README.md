# weatherify
a c# weather app with city search, where https://github.com/statueofdavid/weather-app is the inspo.

## Requirements
### Install .NET core
I used the m$oft install guide for debian 12 -> https://learn.microsoft.com/en-us/dotnet/core/install/linux-debian
+ run-time version: 8.0.303
+ sdk version: 8.0.7
### Install Entity Framework
I used the m$oft install guide for cli -> https://learn.microsoft.com/en-us/ef/core/get-started/overview/install
+ ef-core: 8.0.7
### Install SQLite
I used the image provided by the debian package manager
+ sudo apt update && sudo apt upgrade
+ sudo apt install sqlite3
+ sqlite3 version: 3.40.1


## to Run Locally
+ ```git clone <path.git>```
+ ```cd Weatherify```
+ ```dotnet watch```
