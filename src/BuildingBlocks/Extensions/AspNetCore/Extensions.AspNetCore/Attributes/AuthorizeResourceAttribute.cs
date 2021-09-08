using System;
using Microsoft.AspNetCore.Mvc;

namespace Extensions.AspNetCore
{
  public class AuthorizeResourceAttribute : TypeFilterAttribute
  {
    public AuthorizeResourceAttribute(Type recourceType, Type requirementType, int failStatusCode, string errorMessage)
        : base(typeof(AuthorizeResourceFilter))
    {
      Arguments = new object[] { recourceType, requirementType, failStatusCode, errorMessage };
    }
  }
}
