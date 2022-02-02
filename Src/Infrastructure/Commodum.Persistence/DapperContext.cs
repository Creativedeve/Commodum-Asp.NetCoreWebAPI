using Commodum.Application.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Commodum.Persistence
{
    public class DapperContext : IDBContext
    {
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public int Execute(string query, object param)
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                var result = _connection.Execute(query, param);
                return result;

            }
        }

        public int Execute(string query, object param, CommandType commandType)
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                var result = _connection.Execute(query, param, null, null, commandType);
                return result;
            }
        }
        public T QuerySingleOrDefault<T>(string query, object param)
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                return _connection.QuerySingleOrDefault<T>(query, param);
            }

        }
        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> QueryMultiple<T1, T2, T3>(string query, object param, CommandType commandType)
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                var result = _connection.QueryMultiple(query, param, null, null, commandType);
                var res1 = result.Read<T1>();
                var res2 = result.Read<T2>();
                var res3 = result.Read<T3>();
                return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>>(res1, res2, res3);
            }

        }
        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> QueryMultiple<T1, T2, T3, T4, T5>(string query, object param, CommandType commandType)
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                var result = _connection.QueryMultiple(query, param, null, null, commandType);
                var res1 = result.Read<T1>();
                var res2 = result.Read<T2>();
                var res3 = result.Read<T3>();
                var res4 = result.Read<T4>();
                var res5 = result.Read<T5>();
                return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>>(res1, res2, res3, res4, res5);
            }

        }
        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>> QueryMultiple<T1, T2, T3, T4, T5, T6>(string query, object param, CommandType commandType)
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                var result = _connection.QueryMultiple(query, param, null, null, commandType);
                var res1 = result.Read<T1>();
                var res2 = result.Read<T2>();
                var res3 = result.Read<T3>();
                var res4 = result.Read<T4>();
                var res5 = result.Read<T5>();
                var res6 = result.Read<T6>();
                return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>>(res1, res2, res3, res4, res5, res6);
            }

        }
        private int IList<T>()
        {
            throw new NotImplementedException();
        }

        public T QuerySingleOrDefault<T>(string query, object param, CommandType commandType)
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {

                return _connection.QuerySingleOrDefault<T>(query, param, null, null, commandType);
            }
        }

        public IEnumerable<T> Query<T>(string query, object param)
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                var result = _connection.Query<T>(query, param);
                return result;
            }
        }

        public IEnumerable<T> Query<T>(string query, object param, CommandType commandType)
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                var result = _connection.Query<T>(query, param, null, true, null, commandType);
                return result;
            }
        }

        public IEnumerable<T> Query<T>(string query, CommandType commandType)
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {

                var result = _connection.Query<T>(query, null, null, true, null, commandType);
                return result;


            }
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> QueryMultiple<T1, T2, T3, T4>(string query, object param, CommandType commandType)
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                var result = _connection.QueryMultiple(query, param, null, null, commandType);
                var res1 = result.Read<T1>();
                var res2 = result.Read<T2>();
                var res3 = result.Read<T3>();
                var res4 = result.Read<T4>();
                return new Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>>(res1, res2, res3, res4);
            }

        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> QueryMultiple<T1, T2>(string query, object param, CommandType commandType)
        {
            using (SqlConnection _connection = new SqlConnection(_connectionString))
            {
                var result = _connection.QueryMultiple(query, param, null, null, commandType);
                var res1 = result.Read<T1>();
                var res2 = result.Read<T2>();
                return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(res1, res2);
            }
        }
    }
}

