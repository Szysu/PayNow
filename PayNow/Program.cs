using Avalonia;
using Avalonia.ReactiveUI;
using System;
using Ab4d.Assimp;
using Microsoft.Extensions.Configuration;
using Serilog;
using Log = Serilog.Log;

namespace PayNow;

sealed class Program
{
    public static IConfiguration Configuration { get; private set; } = null!;

    [STAThread]
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
            .CreateLogger();

        try
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Cannot read appsettings.json file. Please check the file.");
            Environment.Exit(1);
            return;
        }

        try
        {
            Ab4d.SharpEngine.Licensing.SetLicense(
                licenseOwner: Configuration["Ab4d:LicenseOwner"],
                licenseType: Configuration["Ab4d:LicenseType"],
                platforms: Configuration["Ab4d:Platforms"],
                license: Configuration["Ab4d:License"]);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Cannot set Ab4d license. Please check the appsettings.json file.");
            Environment.Exit(1);
            return;
        }

        try
        {
            AssimpLibrary.Instance.Initialize("dll/Assimp64.dll");
        } catch (Exception ex) {
            Log.Fatal(ex, "Cannot initialize Assimp library. Please check that the Assimp64.dll file is in the dll folder");
            Environment.Exit(1);
            return;
        }

        System.Threading.Thread.CurrentThread.CurrentCulture = new("en-US");

        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}