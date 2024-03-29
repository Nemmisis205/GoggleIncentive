﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GI.Model.Models;

namespace GI.Repository.Interfaces
{
    public interface ITimeslipRepo : IGenericRepository<Timeslip>
    {
        List<Timeslip> GetByOperator(int id);
    }
}
