using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFCExporter.RevitCommand
{
    public interface ILevelMapper
    {
        string Map(string inputLevelName);
    }
}
