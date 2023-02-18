using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GI.Model.Models
{
    public class Goggle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuotedCycle { get; set; }
        public int PerBox { get; set; } = 72;
        public int BoxesPerPallet { get; set; } = 24;

        public Goggle()
        {

        }

        public Goggle(string name, int quote, int perBox, int boxesPerPallet)
        { 
            Name = name;
            QuotedCycle = quote;
            PerBox = perBox;
            BoxesPerPallet = boxesPerPallet;
        }
    }
}
