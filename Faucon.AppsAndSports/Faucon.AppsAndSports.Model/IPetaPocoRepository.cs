using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Persistence;

namespace Faucon.AppsAndSports.Model
{
    interface IRepository
    {
        T Single<T>(object primaryKey);
        IEnumerable<T> Query<T>();
        Page<T> PagedQuery<T>(long pageNumber, long itemsPerPage, string sql, params object[] args);
        int Insert(object itemToAdd);
        int Update(object itemToUpdate, object primaryKeyValue);
        int Delete<T>(object primaryKeyValue);
    }
}
