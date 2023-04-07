using System;
using System.Collections.Generic;
using System.Text;

namespace NotoriousClient.Framework.Web.Client.Converters
{
    public interface IJsonConverter
    {
        string ConvertToJson(object obj);
    }
}
