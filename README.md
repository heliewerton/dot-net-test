# dot-net-test

You can configure this project from source code or execute from the ./Program/bin/Release/**.
In this directory (./Program/bin/Release), you have releases for ubuntu.16.10-x64, osx.10.11-x64 and win10-x64.

## Run from ./Program/bin/Release/**

In ./Program/bin/Release/netcoreapp2.0/ubuntu.16.10-x64:

```
export TARGET_URL="<Api_Url>"
export TOKEN_CREDENTIALS="<Credentials>"

./Program
```

or install .net core and run with:

```
dotnet run
```

Note: Configue api url with http://domain.com without "/" in the end.
Note: Configue api crendentials conform test requirements.

## To Run Tests

In ./Domain.Tests:

```
dotnet test
```

### Notes

The search is configured to run with the following values, but you can change :).

```
// Method found in ./Program/Program.cs.

return new Search()
{
    Language = "ENG",
    Currency = "USD",
    Destination = "MCO",
    
    DateFrom = DateTime.Now.ToString("MM/dd/yyyy"),
    DateTo = DateTime.Now.ToString("MM/dd/yyyy"),

    SearchOccupancy = new SearchOccupancy()
    {
        AdultCount = "1",
        ChildCount = "1",
        ChildAges = new string[] { "10" },
    }
};
```