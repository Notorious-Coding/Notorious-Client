using System;
using System.Collections.Generic;
using System.Text;

namespace NotoriousClient.Framework.Web.Client.Builder
{
    public class BodyNotCompatibleException : Exception
    {
        public BodyNotCompatibleException() : base("Le body que vous utilisez n'est pas compatible avec la requête en cours.")
        {
            
        }
    }
}
