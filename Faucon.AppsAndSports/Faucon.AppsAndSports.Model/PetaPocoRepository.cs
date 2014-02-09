using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Persistence;

namespace Faucon.AppsAndSports.Model
{
    public class PetaPocoRepository : IRepository
    {
        private readonly Database _db;

        public PetaPocoRepository() :
            this("AppsAndSports")
        {
        }

        public PetaPocoRepository(string connectionStringName)
        {
            _db = new Database(ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString);
        }
        public TPassType Single<TPassType>(object primaryKey)
        {
            return _db.Single<TPassType>(primaryKey);
        }
        public IEnumerable<TPassType> Query<TPassType>()
        {
            var pd = Database.PocoData.ForType(typeof(TPassType));

            var sql = "SELECT * FROM " + pd.TableInfo.TableName;

            return _db.Query<TPassType>(sql);
        }
        public IEnumerable<TPassType> Query<TPassType>(string where = "", string orderBy = "", int limit = 0, string columns = "*", params object[] args)
        {
            var pd = Database.PocoData.ForType(typeof(TPassType));

            string sql = BuildSql(pd.TableInfo.TableName, where, orderBy, limit, columns);

            return Query<TPassType>(sql, args);
        }
        public IEnumerable<TPassType> Query<TPassType>(string sql, params object[] args)
        {
            return _db.Query<TPassType>(sql, args);
        }
        public Page<TPassType> PagedQuery<TPassType>(long pageNumber, long itemsPerPage, string sql, params object[] args)
        {
            return _db.Page<TPassType>(pageNumber, itemsPerPage, sql, args) as Page<TPassType>;
        }
        public Page<TPassType> PagedQuery<TPassType>(long pageNumber, long itemsPerPage, Sql sql)
        {
            return _db.Page<TPassType>(pageNumber, itemsPerPage, sql) as Page<TPassType>;
        }
        public int Insert(object poco)
        {
            return (int)_db.Insert(poco);
        }
        public int Insert(string tableName, string primaryKeyName, bool autoIncrement, object poco)
        {
            return (int)_db.Insert(tableName, primaryKeyName, autoIncrement, poco);
        }
        public int Insert(string tableName, string primaryKeyName, object poco)
        {
            return (int)_db.Insert(tableName, primaryKeyName, poco);
        }
        public int Update(object poco)
        {
            return _db.Update(poco);
        }
        public int Update(object poco, object primaryKeyValue)
        {
            return _db.Update(poco, primaryKeyValue);
        }
        public int Update(string tableName, string primaryKeyName, object poco)
        {
            return _db.Update(tableName, primaryKeyName, poco);
        }
        public int Delete<TPassType>(object pocoOrPrimaryKey)
        {
            return _db.Delete<TPassType>(pocoOrPrimaryKey);
        }
        public static string BuildSql(string tableName, string where = "", string orderBy = "", int limit = 0, string columns = "*")
        {
            string sql = limit > 0 ? "SELECT TOP " + limit + " {0} FROM {1} " : "SELECT {0} FROM {1} ";
            if (!string.IsNullOrEmpty(where))
                sql += where.Trim().StartsWith("where", StringComparison.CurrentCultureIgnoreCase) ? where : "WHERE " + where;
            if (!String.IsNullOrEmpty(orderBy))
                sql += orderBy.Trim().StartsWith("order by", StringComparison.CurrentCultureIgnoreCase) ? orderBy : " ORDER BY " + orderBy;
            return string.Format(sql, columns, tableName);
        }
    }
}
