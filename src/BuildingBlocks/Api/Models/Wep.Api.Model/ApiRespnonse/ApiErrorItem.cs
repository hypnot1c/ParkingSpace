using System;
using System.ComponentModel;
using System.Reflection;
using Newtonsoft.Json;

namespace Web.Api.Model
{
  public class ApiErrorItem
  {
    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("code", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int Code { get; set; }

    public ApiErrorItem()
    {

    }

    public ApiErrorItem(Enum codeError)
    {
      var type = codeError.GetType();

      FieldInfo fi = type.GetField(codeError.ToString());
      DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
      Message = attributes.Length > 0 ? attributes[0].Description : codeError.ToString();

      Array enums = Enum.GetValues(type);
      int index = Array.IndexOf(enums, codeError);
      Code = (int)enums.GetValue(index);
    }
  }
}
