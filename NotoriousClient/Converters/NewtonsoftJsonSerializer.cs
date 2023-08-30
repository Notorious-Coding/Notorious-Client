using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotoriousClient.Converters
{
    /// <summary>
    /// Serialize object to JSON using NewtonsoftJson.
    /// </summary>
    public class NewtonsoftJsonSerializer : IJsonSerializer
    {
        public string ConvertToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
