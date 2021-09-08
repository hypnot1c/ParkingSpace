using System;

namespace PS.Web.Api.Client
{
  public class DefaultForAttribute : Attribute
  {
    public DefaultForAttribute(Type @abstract)
    {
      this.Abstract = @abstract;
    }

    public Type Abstract { get; }
  }
}
