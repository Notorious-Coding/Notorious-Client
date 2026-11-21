using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NotoriousClient.Tests.Unit.Attributes
{
    public class GWTFact : FactAttribute
    {
        public GWTFact(
            string given,
            string when,
            string then,
            [CallerFilePath] string? sourceFilePath = null,
            [CallerLineNumber] int sourceLineNumber = -1) : base(sourceFilePath, sourceLineNumber)
        {
            DisplayName = $"Given {given} \nWhen {when} \nThen {then}";
        }
    }
}
