# BoboTech.Logger
NLog wrapper

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

Have fun.