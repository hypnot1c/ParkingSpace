using System;
using System.Collections.Generic;
using System.Linq;
using Extensions.AspNetCore;
using Extensions.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using PS.Data.Master.Context;
using PS.DataService;

namespace PS.Web.Api.Resources
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddAndConfigureMvc(this IServiceCollection services)
    {
      services.AddControllers(opt =>
      {
        var cacheProfile = new CacheProfile()
        {
          NoStore = true
        };
        opt.CacheProfiles.Add("Default", cacheProfile);
        opt.Filters.Add(new AuthorizeFilter());
        opt.Filters.Add<GlobalExceptionFilter>();
      })
      .AddFluentValidation(fv =>
      {
        fv.RegisterValidatorsFromAssemblyContaining<Model.Input.AssemblyMarker>();
        fv.ImplicitlyValidateChildProperties = true;
        fv.DisableDataAnnotationsValidation = true;
        ValidatorOptions.Global.CascadeMode = CascadeMode.Stop;
      })
      .AddNewtonsoftJson(o =>
      {
        o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
      })
      .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
      ;

      return services;
    }

    public static IServiceCollection AddDbContexts(
      this IServiceCollection services,
      IConfiguration config
      )
    {
      services.AddDbContext<MasterContext>(
        option => option.UseSqlServer(
          config.GetConnectionString(nameof(MasterContext))
          )
        );

      return services;
    }

    public static IServiceCollection AddDataService(
      this IServiceCollection services
      )
    {
      services.AddScoped<IDataService, DataService.DataService>();

      services.AddScoped<IUsersDataService, UsersDataService>();

      return services;
    }

    public static IServiceCollection AddAuthenticationAndAuthorization(
        this IServiceCollection services,
        IConfiguration config
        )
    {
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opts => opts
          .UseGoogle(
            clientId: config.GetValue<string>("services:Google:clientId")
            )
          .Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
          {
            OnTokenValidated = JwtBearerEvents.OnTokenValidated
          })
        ;

      services.AddAuthorization(opts =>
      {
        opts.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
          .RequireAuthenticatedUser()
          .Build();
      });

      services.AddAuthorizationHandlers(typeof(Program));

      return services;
    }

    public static IServiceCollection AddAuthorizationHandlers(this IServiceCollection services, params Type[] assemblyMarkerTypes)
    {
      var type = typeof(AuthorizationHandler<>);

      var addedTypes = new HashSet<Type>();
      foreach (var assemblyMaker in assemblyMarkerTypes)
      {
        var handlerTypes = assemblyMaker.Assembly.GetTypes()
          .Where(t =>
            t.IsClass
            &&
            !t.IsAbstract
            &&
            t.BaseType != null
            &&
            t.InheritsFrom(type)
          )
          .ToList()
          ;

        foreach (var entryType in handlerTypes)
        {
          var rootHandlerType = entryType.BaseType;
          while (rootHandlerType.BaseType != null && rootHandlerType.BaseType.IsGenericType)
          {
            rootHandlerType = rootHandlerType.BaseType;
          }
          if (!addedTypes.Contains(rootHandlerType))
          {
            services.AddScoped(typeof(IAuthorizationHandler), entryType);
            addedTypes.Add(rootHandlerType);
          }
        }
      }

      return services;
    }
  }
}
