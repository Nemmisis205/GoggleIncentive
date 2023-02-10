using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GI.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IGoggleRepo Goggles { get; }
        ITimeslipRepo Timeslips { get; }
        IOperatorRepo Operators { get; }
    }
}
