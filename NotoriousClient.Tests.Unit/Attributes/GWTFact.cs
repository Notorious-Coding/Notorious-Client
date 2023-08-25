using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotoriousClient.Tests.Unit.Attributes
{
    public class GWTFact : FactAttribute
    {
        public GWTFact(string given, string when, string then)
        {
            DisplayName = $"Given {given} \nWhen {when} \nThen {then}";
        }
    }
}
