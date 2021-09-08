using AutoMapper;
using Diagnostics.Abstractions;
using Extensions.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PS.Web.Api.Resources;

namespace PS.Web.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      this.Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddOptions();

      services.AddHttpContextAccessor();

      services.AddDbContexts(this.Configuration);

      services.AddMediatR(typeof(Startup));

      services.AddAutoMapper(typeof(Program).Assembly);

      services.AddScoped<ICorrelationIdProvider, HttpContextCorrelationIdProvider>();

      services.AddMemoryCache();

      services.AddDataService();

      services.AddAndConfigureMvc();

      services.AddAuthenticationAndAuthorization();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper mapper)
    {
      mapper.ConfigurationProvider.AssertConfigurationIsValid();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      var logRequestTime = this.Configuration.GetValue<bool>("Logging:LogHttpRequestExecTime");
      app.UseCorrelationIdLogging(logRequestTime);

      //app.UseAuthentication();
      //app.UseAuthorization();

      app.UseAPIResponseWrapperMiddleware("/v1");

      app.UseEndpoints(endpoints =>
      {
        endpoints
          .MapControllers()
          .AllowAnonymous()
          //.RequireAuthorization()
          ;
      });
    }
  }
}
