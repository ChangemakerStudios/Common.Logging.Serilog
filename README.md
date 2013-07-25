Common.Logger.Serilog
=====
Provides a bridge from the old to the new logging systems.
-----

### Usage

### Configure app.config/web.config file of your project:

```
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
```

You must configure and provide a global logger to use this adapter:

```
var log = new LoggerConfiguration()
    .WriteTo.ColoredConsole()
    .CreateLogger();

// set global instance of Serilog logger which Common.Logger.Serilog requires.
Log.Logger = log;
```

### Links

* [Common Logging](http://netcommon.sourceforge.net/ "Common Infrastructure Libraries for .NET")
* [Serilog Project](http://serilog.net/ "Serilog")