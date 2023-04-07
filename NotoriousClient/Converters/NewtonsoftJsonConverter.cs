using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotoriousClient.Framework.Web.Client.Converters
{
    /// <summary>
    /// Permet de convertir un objet en string a partir du serializer Newtonsoft.
    /// </summary>
    public class NewtonsoftJsonConverter : IJsonConverter
    {
        public string ConvertToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
