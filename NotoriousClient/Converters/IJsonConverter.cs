using System;
using System.Collections.Generic;
using System.Text;

namespace NotoriousClient.Converters
{
    /// <summary>
    /// JSON converter.
    /// </summary>
    public interface IJsonConverter
    {
        /// <summary>
        /// Serialize object to JSON string.
        /// </summary>
        /// <param name="obj">object to serialize.</param>
        /// <returns>JSON string</returns>
        string ConvertToJson(object obj);
    }
}
