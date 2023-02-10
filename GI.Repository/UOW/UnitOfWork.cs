using GI.Repository.Interfaces;
using GI.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace GI.Repository.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public IGoggleRepo Goggles { get; set; }
        public IOperatorRepo Operators { get; set; }
        public ITimeslipRepo Timeslips { get; set; }

        public UnitOfWork(ConnectionFactory conn)
        {
            Goggles = new GoggleRepo(conn);
            Operators = new OperatorRepo(conn);
            Timeslips = new TimeslipRepo(conn);
        }
    }
}
