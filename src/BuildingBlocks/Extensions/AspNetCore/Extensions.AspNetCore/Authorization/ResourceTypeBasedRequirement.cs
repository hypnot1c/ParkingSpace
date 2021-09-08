using System;
using System.Collections.Generic;

namespace Extensions.AspNetCore
{
  public class ResourceTypeBasedRequirement
  {
    public ResourceTypeBasedRequirement(Type targetResourceType)
    {
      this.TargetResourceType = targetResourceType;
    }

    public ResourceTypeBasedRequirement(Type targetResourceType, IDictionary<string, object> actionArguments)
    {
      this.TargetResourceType = targetResourceType;
      this.ActionArguments = actionArguments;
    }

    public Type TargetResourceType { get; }

    public IDictionary<string, object> ActionArguments { get; }
  }
}
