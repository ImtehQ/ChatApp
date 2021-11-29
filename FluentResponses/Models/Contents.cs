using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentResponses.Models
{
    public class Contents
    {
        internal object Content { get; init; }
        internal Contents(object content)
        {
            Content = content;
        }
    }
}
