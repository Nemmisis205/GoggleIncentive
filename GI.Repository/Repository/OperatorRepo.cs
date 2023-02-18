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
    public class OperatorRepo : IOperatorRepo
    {
        private readonly ConnectionFactory _connectionFactory;
        public OperatorRepo(ConnectionFactory conn)
        {
            _connectionFactory = conn;
        }
        public int Add(Operator entity)
        {
            using (var cnn = _connectionFactory.GetConnection())
            {
                var sql = "INSERT INTO Operators(Name) VALUES (@Name)";
                cnn.Open();
                int result = cnn.Execute(sql, entity);
                return result;
            }
        }

        public int Delete(Operator entity)
        {
            var sql = @"Delete FROM Operators WHERE Name = @Name";
            using (var cnn = _connectionFactory.GetConnection())
            {
                cnn.Open();
                var result = cnn.Execute(sql, entity);
                return result;
            }
        }

        public List<Operator> GetAll()
        {
            var sql = @"SELECT * From Operators";
            using (var cnn = _connectionFactory.GetConnection())
            {
                cnn.Open();
                var result = cnn.Query<Operator>(sql).ToList();
                return result;
            }
        }

        public Operator GetById(int id)
        {
            var sql = @"SELECT * FROM Operators WHERE Id = @Id";
            using (var cnn = _connectionFactory.GetConnection())
            {
                cnn.Open();
                var result = cnn.QuerySingleOrDefault<Operator>(sql, new { Id = id });
                return result;
            }
        }

        public int Update(Operator entity)
        {
            var sql = @"UPDATE Operators SET Name=@Name WHERE Id = @Id";
            using (var cnn = _connectionFactory.GetConnection())
            {
                cnn.Open();
                var result = cnn.Execute(sql, entity);
                return result;
            }
        }

        public Operator GetByName(string name)
        {
            var sql = @"SELECT * FROM Operators WHERE Name = @Name";
            using (var cnn = _connectionFactory.GetConnection())
            {
                cnn.Open();
                var result = cnn.QuerySingleOrDefault<Operator>(sql, new { Name = name });
                return result;
            }
        }
    }
}
