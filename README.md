# BoboTech.Logger
NLog wrapper

BoboTech.Logger can be downloaded from [NuGet](https://www.nuget.org/packages/BoboTech.Logger) and be searched in the NuGet Package Manager by the tag BoboTech.

It exposes the static class Log in the namespace BoboTech.Logger. The class has the following methods:

```csharp
var caller = "something";
Log.Trace("Trace message", caller);
Log.Debug("Debug message", caller);
Log.Info("Info message", caller);
Log.Warn("Warn message", caller);
Log.Error("Error message", caller);
Log.Error(new Exception("First exception test"), "Exception message.", caller);
Log.Error(new Exception("Second exception test"), "Exception message with error id.", caller, $"{DateTime.Now:yyyyMMdd_hhmmss}");
Log.Error(new Exception("Third exception test"), $"Exception message{Environment.NewLine}with new line.", caller);
```

Have fun.