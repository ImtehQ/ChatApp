using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentResponses.Models
{
    public class Status
    {
        internal bool Value { get; set; }
        internal Status(bool value)
        {
            Value = value;
        }

    }
}
