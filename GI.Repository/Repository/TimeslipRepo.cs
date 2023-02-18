using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using GI.Model.Models;
using GI.Repository.Interfaces;
using GI.Repository.UOW;

namespace GI.Repository.Repository
{
    public class TimeslipRepo : ITimeslipRepo
    {
        private readonly ConnectionFactory _connectionFactory;
        public TimeslipRepo(ConnectionFactory conn)
        {
            _connectionFactory = conn;
        }
        public int Add(Timeslip entity)
        {
            var sql = @"INSERT INTO Timeslips
                        (OperatorId, GoggleId, StartTime, EndTime, StartCounter, EndCounter, StartBoxCount, EndBoxCount, StartPieceCount, EndPieceCount,
                        CyclesPerHour, Scrap, ScrapPercent, GoodParts, CycleCount, HoursRan, Efficiency, IncentiveAchieved, Override) 
                        VALUES
                        (@OperatorId, @GoggleId, @StartTime, @EndTime, @StartCounter, @EndCounter, @StartBoxCount, @EndBoxCount, @StartPieceCount, @EndPieceCount,
                        @CyclesPerHour, @Scrap, @ScrapPercent, @GoodParts, @CycleCount, @HoursRan, @Efficiency, @IncentiveAchieved, @Override)";
            using (var cnn = _connectionFactory.GetConnection())
            {
                cnn.Open();
                int result = cnn.Execute(sql, entity);
                return result;
            }
        }

        public int Delete(Timeslip entity)
        {
            var sql = @"DELETE FROM Timeslips WHERE Id = @Id";
            using (var cnn = _connectionFactory.GetConnection())
            {
                cnn.Open();
                var result = cnn.Execute(sql, entity);
                return result;
            }
        }

        public List<Timeslip> GetAll()
        {
            var sql = @"SELECT * From Timeslips";
            using (var cnn = _connectionFactory.GetConnection())
            {
                cnn.Open();
                var result = cnn.Query<Timeslip>(sql).ToList();
                return result;
            }
        }

        public Timeslip GetById(int id)
        {
            var sql = @"SELECT * FROM Timeslips WHERE Id = @Id";
            using (var cnn = _connectionFactory.GetConnection())
            {
                cnn.Open();
                var result = cnn.QuerySingleOrDefault<Timeslip>(sql, new { Id = id });
                return result;
            }
        }

        public int Update(Timeslip entity)
        {
            var sql = @"UPDATE Timeslips SET OperatorId = @OperatorId, GoggleId = @GoggleId, StartTime = @StartTime, EndTime = @EndTime, StartCounter = @StartCounter, 
                        EndCounter = @EndCounter, StartBoxCount = @StartBoxCount, EndBoxCount = @EndBoxCount, StartPieceCount = @StartPieceCount, EndPieceCount = @StartPieceCount,
                        CyclesPerHour = @CyclesPerHour, Scrap = @Scrap, ScrapPercent = @ScrapPercent, GoodParts = @GoodParts, CycleCount = @CycleCount, HoursRan = @HoursRan, 
                        Efficiency = @Efficiency, IncentiveAchieved = @IncentiveAchieved, Override = @Override WHERE Id = @Id";
            using (var cnn = _connectionFactory.GetConnection())
            {
                cnn.Open();
                var result = cnn.Execute(sql, entity);
                return result;
            }
        }

        public List<Timeslip> GetByOperator(int id)
        {
            var sql = "SELECT * FROM Timeslips WHERE OperatorId = @OperatorId";
            using (var cnn = _connectionFactory.GetConnection())
            {
                cnn.Open();
                var result = cnn.Query<Timeslip>(sql, new { OperatorId = id }).ToList();
                return result;
            }
        }
    }
}
