using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotoriousClient.Converters
{
    /// <summary>
    /// Convert object to JSON using NewtonsoftJson.
    /// </summary>
    public class NewtonsoftJsonConverter : IJsonConverter
    {
        public string ConvertToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
