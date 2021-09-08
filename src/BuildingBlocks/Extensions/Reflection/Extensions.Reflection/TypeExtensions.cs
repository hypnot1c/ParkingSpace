using System;
using System.Linq;

namespace Extensions.Reflection
{
  public static class TypeExtensions
  {
    public static bool InheritsFrom(this Type type, Type baseType)
    {
      // null does not have base type
      if (type == null)
      {
        return false;
      }

      // only interface or object can have null base type
      if (baseType == null)
      {
        return type.IsInterface || type == typeof(object);
      }

      // check implemented interfaces
      if (baseType.IsInterface)
      {
        return type.GetInterfaces()
          .Any(i =>
          {
            if (!i.IsGenericType)
            {
              return i == baseType;
            }

            return i.GetGenericTypeDefinition() == baseType;
          });
      }

      // check all base types
      var currentType = type;
      while (currentType != null)
      {
        if (currentType.IsGenericType)
        {
          var res = (currentType.GetGenericTypeDefinition() == baseType) || (currentType.BaseType.InheritsFrom(baseType));
          return res;
        }
        else
        {
          if (currentType.BaseType == baseType)
          {
            return true;
          }
        }

        currentType = currentType.BaseType;
      }

      return false;
    }
  }
}
