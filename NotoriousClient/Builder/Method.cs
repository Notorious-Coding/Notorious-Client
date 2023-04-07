using System;
using System.Collections.Generic;
using System.Text;

namespace NotoriousClient.Framework.Web.Client.Builder
{
    /// <summary>
    /// Verbe HTTP.
    /// </summary>
    public enum Method
    {
        /// <summary>
        /// Verbe GET.
        /// </summary>
        Get,
        /// <summary>
        /// Verbe POST.
        /// </summary>
        Post,
        /// <summary>
        /// Verbe PUT.
        /// </summary>
        Put,
        /// <summary>
        /// Verbe PATCH.
        /// </summary>
        Patch,
        /// <summary>
        /// Verbe DELETE.
        /// </summary>
        Delete
    }
}
