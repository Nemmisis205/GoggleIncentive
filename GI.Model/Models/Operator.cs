﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GI.Model.Models
{
    public class Operator
    {
        int Id { get; set; }
        string Name { get; set; }
        //List<Timeslip> timeslips = new List<Timeslip>();
        //Unsure if I need to have the list there or not at this moment. I'm using the Id as a Foreign Key in Timeslips
    }
}
