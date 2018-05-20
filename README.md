# authServer
Authorization server using IdentityServer4. How to add administrators via seed, please see in [Seed Data](#SeedData) section.
In project Code First approach is used.

[![Build Status](https://andriusv.visualstudio.com/_apis/public/build/definitions/1d17d37a-60d8-4321-9449-cc1f0b28adf0/4/badge)](https://andriusv.visualstudio.com/authServer/_build/index?definitionId=4)

## To Run Locally
Choose your location for the project in CommandPrompt. Set correct path:
```
cd <path>
```
Run command:
```
git clone https://github.com/andriusv/authServer.git
```
To restore packages navigate to the project folder and run command in _CommandPrompt_ or in Visual Studio _Package Manager Console_:
```
cd <path to project>
dotnet restore
```
In appsettings.json change DefaultDbContext to desired one:
```
"DefaultDbContext": "Data Source=<Server>;Initial Catalog=<Database>;User Id=<userName>;Password=<Password>"
```

In appsettings-custom.json change data to desired:
```
{
  "AppSettings": {
    "AdminName": "<userName>",
    "AdminEmail": "<email>",
    "AdminPassword": "<password>"
  }
}
```

## <a name="SeedData"></a>Seed Data
In AuthService it is possible to add users via seed. Administrator is set in Data/DbInitializer.cs

## Test Auth Service
Go to <this project>/swagger
