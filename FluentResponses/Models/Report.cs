using FluentResponses.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentResponses.Models
{
    public class Report
    {
        internal string Content { get; init; }
        internal Report(string content)
        {
            Content = content;
        }
    }
}
