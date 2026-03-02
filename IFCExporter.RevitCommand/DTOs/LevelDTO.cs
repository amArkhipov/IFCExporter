using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFCExporter.RevitCommand
{
    public class LevelDTO
    {
        public string Name { get; set; }
        public int LevelId { get; set; }
        public LevelDTO(string Name, int LevelId)
        {
            this.Name = Name;
            this.LevelId = LevelId;
        }
    }
}
