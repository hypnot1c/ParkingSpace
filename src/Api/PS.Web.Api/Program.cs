using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace PS.Web.Api
{
  /// <summary>
  /// 
  /// </summary>
  public class Program
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    public static async Task Main(string[] args)
    {
      await BuildHost(args).RunAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static IWebHost BuildHost(string[] args)
    {
      return WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .ConfigureAppConfiguration(ConfigureAppConfiguration)
        .ConfigureLogging(ConfigureLogging)
        .Build()
        ;
    }

    private static void ConfigureAppConfiguration(WebHostBuilderContext hostingContext, IConfigurationBuilder config)
    {
      var env = hostingContext.HostingEnvironment;

      config
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
        ;
    }

    private static void ConfigureLogging(WebHostBuilderContext hostingContext, ILoggingBuilder logging)
    {
      logging.ClearProviders();

      var env = hostingContext.HostingEnvironment;
      logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));

      if (env.IsDevelopment())
      {
        logging.AddDebug();
        logging.AddConsole();
      }

      logging.AddNLog($"nlog.{env.EnvironmentName}.config");
    }
  }
}
