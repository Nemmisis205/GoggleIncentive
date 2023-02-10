using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GI.Model.Models
{
    public class Goggle
    {
        int Id { get; set; }
        public string Name { get; set; }
        public int QuotedCycle { get; set; }
        public int PerBox { get; set; } = 72;
    }
}
