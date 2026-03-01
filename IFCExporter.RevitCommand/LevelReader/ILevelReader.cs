using IFCExporter.RevitCommand.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFCExporter.RevitCommand.LevelReader
{
    public interface ILevelReader
    {
        public List<LevelDTO> ReadAll();
    }
}
