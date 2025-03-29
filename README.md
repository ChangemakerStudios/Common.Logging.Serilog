Common.Logging.Serilog
=====

[![NuGet version](https://badge.fury.io/nu/Common.Logging.Serilog.svg)](https://badge.fury.io/nu/Common.Logging.Serilog) [![Build and Push to Nuget](https://github.com/ChangemakerStudios/Common.Logging.Serilog/actions/workflows/main.yml/badge.svg)](https://github.com/ChangemakerStudios/Common.Logging.Serilog/actions/workflows/main.yml)

*Common.Logging.Serilog* is an adapter that bridges the [Common.Logging](https://netcommon.sourceforge.net/) abstraction with [Serilog](https://serilog.net/), allowing you to use Serilog as the underlying logging framework in applications that rely on Common.Logging.

## Installation

Install the package via NuGet Package Manager:

```shell
Install-Package Common.Logging.Serilog
```

Or using the .NET CLI:

```shell
dotnet add package Common.Logging.Serilog
```

## Getting Started

### Configuring Common.Logging

Update your `app.config` or `web.config` file to use the Serilog factory adapter:

```xml
<configuration>
  <configSections>
    <sectionGroup name="common">
      <section name="logging" type="Common.Logging.ConfigurationSectionHandler, Common.Logging" />
    </sectionGroup>
  </configSections>

  <common>
    <logging>
      <factoryAdapter type="Common.Logging.Serilog.SerilogFactoryAdapter, Common.Logging.Serilog" />
    </logging>
  </common>
</configuration>
```

### Setting Up Serilog & Usage Example

Configure Serilog and assign it to the global `Log.Logger` instance:

```csharp
using Serilog;
using Common.Logging;

class Program
{
    static void Main(string[] args)
    {
        // Configure Serilog: -- Must set global Log.Logger instance
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        // Obtain a Common.Logging logger
        var logger = LogManager.GetLogger<Program>();
        logger.Info("Application has started.");

        // Your application code here

        // Ensure to flush and close Serilog at the end
        Log.CloseAndFlush();
    }
}
```

## License

This project is licensed under the [MIT License](https://mit-license.org/).
