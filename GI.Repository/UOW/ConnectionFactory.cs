using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using GI.Repository.Interfaces;
using Microsoft.Data.Sqlite;

namespace GI.Repository.UOW
{
    public class ConnectionFactory
    {
        public string DbFile
        {
            get { return Environment.CurrentDirectory + "\\GIDB.sqlite"; }
        }

        public SqliteConnection GetConnection()
        {
            return new SqliteConnection("Data Source=" + DbFile);
        }

        public ConnectionFactory()
        {
            if (!File.Exists(DbFile))
            {
                using (var cnn = GetConnection())
                {
                    cnn.Open();
                    cnn.Execute(
                        @"create table Goggles
                            (
                                Id              integer primary key AUTOINCREMENT,
                                Name            text UNIQUE,
                                QuotedCycle     integer NOT NULL,
                                PerBox          integer NOT NULL DEFAULT 72,
                                BoxesPerPallet  integer DEFAULT 24
                            )"
                        );
                    cnn.Execute(
                        @"create table Operators
                            (
                                Id              integer primary key AUTOINCREMENT,
                                Name            text UNIQUE NOT NULL
                            )"
                        );
                    cnn.Execute(
                        @"create table Timeslips
                            (
                                Id              integer primary key AUTOINCREMENT,
                                OperatorId      integer NOT NULL,
                                GoggleId        integer NOT NULL,
                                StartTime       int NOT NULL,
                                EndTime         int NOT NULL,
                                StartCounter    integer NOT NULL,
                                EndCounter      integer NOT NULL,
                                StartBoxCount   integer NOT NULL,
                                StartPieceCount integer NOT NULL,
                                EndBoxCount     integer NOT NULL,
                                EndPieceCount   integer NOT NULL,
                                CycleCount      integer NOT NULL,
                                GoodParts       integer NOT NULL,
                                CyclesPerHour   real NOT NULL,
                                Scrap           integer NOT NULL,
                                ScrapPercent    real NOT NULL,
                                HoursRan        int NOT NULL,
                                Efficiency      real NOT NULL,
                                IncentiveAchieved   integer NOT NULL,
                                Override        integer DEFAULT Null
                            )"
                        );
                }
            }
        }
    }
}
