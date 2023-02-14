using Dapper;
using GI.Model.Models;
using GI.Repository.Interfaces;
using GI.Repository.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GI.Repository.Repository
{
    internal class GoggleRepo : IGoggleRepo
    {
        private ConnectionFactory _connectionFactory;
        public GoggleRepo(ConnectionFactory conn)
        {
            _connectionFactory = conn;
        }

        public int Add(Goggle entity)
        {
            var sql = @"INSERT INTO Goggles(Name, QuotedCycle, PerBox) VALUES(@Name, @QuotedCycle, @PerBox)";
            using (var cnn = _connectionFactory.GetConnection())
            {
                cnn.Open();
                int result = cnn.Execute(sql, entity);
                return result;
            }
        }

        public int Delete(Goggle entity)
        {
            var sql = @"Delete FROM Goggles WHERE Id = @Id";
            using (var cnn = _connectionFactory.GetConnection())
            {
                cnn.Open();
                var result = cnn.Execute(sql, entity);
                return result;
            }
        }

        public List<Goggle> GetAll()
        {
            var sql = @"SELECT * From Goggles";
            using (var cnn = _connectionFactory.GetConnection())
            {
                cnn.Open();
                var result = cnn.Query<Goggle>(sql).ToList();
                return result;
            }
        }

        public Goggle GetById(int id)
        {
            var sql = @"SELECT * FROM Goggles WHERE Id = @Id";
            using(var cnn = _connectionFactory.GetConnection())
            {
                cnn.Open();
                var result = cnn.QuerySingleOrDefault<Goggle>(sql, new {Id = id});
                return result;
            }
        }

        public int Update(Goggle entity)
        {
            var sql = @"UPDATE Goggles SET Name=@Name, QuotedCycle=@QuotedCycle, PerBox=@PerBox WHERE Id = @Id";
            using (var cnn = _connectionFactory.GetConnection())
            {
                cnn.Open();
                var result = cnn.Execute(sql, entity);
                return result;
            }
        }
    }
}
