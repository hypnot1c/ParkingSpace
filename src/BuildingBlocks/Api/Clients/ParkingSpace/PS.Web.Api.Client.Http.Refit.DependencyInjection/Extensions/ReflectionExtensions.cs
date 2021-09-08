using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PS.Web.Api.Client
{
  public static class ReflectionExtensions
  {
    public static IEnumerable<Type> HttpInterfaces(this Assembly assembly)
    {
      return assembly
        .GetTypes()
        .Where(t => t.IsInterface)
        .Where(t =>
          t.GetCustomAttributes().Any(a => a.GetType() == typeof(HttpInterfaceAttribute))
        )
        .ToList()
        ;
    }
  }
}
