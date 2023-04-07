using System;
using System.Collections.Generic;
using System.Text;

namespace NotoriousClient.Converters
{
    public interface IJsonConverter
    {
        string ConvertToJson(object obj);
    }
}
