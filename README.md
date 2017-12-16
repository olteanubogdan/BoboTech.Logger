# BoboTech.Logger
NLog wrapper with minimum cofiguration

Usage
-----

BoboTech.Logger can be downloaded from [NuGet - BoboTech.Logger](https://www.nuget.org/packages/BoboTech.Logger) and be searched in the NuGet Package Manager by the tag BoboTech.

It exposes the static class Log in the namespace BoboTech.Logger. The class has the following methods:

```csharp
var caller = $"{nameof(Class)}.{nameof(Method)}";
try
{
  Log.Trace("Trace message", caller);
  Log.Debug("Debug message", caller);
  Log.Info("Info message", caller);
  Log.Warn("Warn message", caller);
  Log.Error("Error message", caller);
}
catch (Exception ex)
{
  Log.Error(ex, "Exception message.", caller);
  Log.Error(ex, "Exception message with error id.", caller, $"{DateTime.Now:yyyyMMdd_hhmmss}");
}
```

Configuration
-------------

There are only four settings available, all in {app/web}.config file:

```xml
<configuration>
  <!--...-->
  <appSettings>
    <!--...-->
    <add key="Log.Level" value="Trace"/>
    <add key="Log.Location" value="CommonApplicationData"/>
    <add key="App.Company" value="BoboTech"/>
    <add key="App.Name" value="Logger"/>
    <!--...-->
  </appSettings>
  <!--...-->
</configuration>
```

If a setting is missing, then a default value will be picked.

Log.Level can be any of the folowing: Trace, Debug, Info, Warn, Error, Fatal. Defaults to Trace if `Debugger.IsAttached`, Debug otherwise.

Log.Location can be any of the `System.Environment.SpecialFolder` enum. Most common are LocalApplicationData or CommonApplicationData. Defaults to CommonApplicationData.

App.Company can be any string and defaults to BoboTech.

App.Name can be any string and defaults to Logger.

Log location
------------

The logs will be saved in `SpecialFolder.{Log.Location}\{App.Company}\{App.Name}\logs\{shortdate}.log`.

Conclusion
----------
Have fun.