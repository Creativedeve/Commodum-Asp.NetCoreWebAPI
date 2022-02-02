using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Commodum.Application.Interfaces
{
    public interface IDBContext
    {
        int Execute(string query, object param);
        int Execute(string query, object param, CommandType commandType);
        T QuerySingleOrDefault<T>(string query, object param);
        T QuerySingleOrDefault<T>(string query, object param, CommandType commandType);
        IEnumerable<T> Query<T>(string query, object param);
        IEnumerable<T> Query<T>(string query, CommandType commandType);
        IEnumerable<T> Query<T>(string query, object param, CommandType commandType);
        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>> QueryMultiple<T1, T2, T3>(string query, object param, CommandType commandType);
        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>> QueryMultiple<T1, T2, T3, T4>(string query, object param, CommandType commandType);
        Tuple<IEnumerable<T1>, IEnumerable<T2>> QueryMultiple<T1, T2>(string query, object param, CommandType commandType);
        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>> QueryMultiple<T1, T2, T3, T4, T5>(string query, object param, CommandType commandType);
        Tuple<IEnumerable<T1>, IEnumerable<T2>, IEnumerable<T3>, IEnumerable<T4>, IEnumerable<T5>, IEnumerable<T6>> QueryMultiple<T1, T2, T3, T4, T5, T6>(string query, object param, CommandType commandType);
    }
}
