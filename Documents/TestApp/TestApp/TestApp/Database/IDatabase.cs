using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestApp.Domain;

namespace TestApp.Database
{
    public interface IDatabase
    {
        SQLiteConnectionString Connection { get; set; }

        Task CreateTables(Type[] tableTypes);

        Task<int> DeleteAllData(string tableName);

        Task<T> RunInTransaction<T>(Func<DatabaseConnection, Task<T>> taskFunc);

        Task<T> Get<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity, new();

        AsyncTableQuery<T> Table<T>() where T : BaseEntity, new();

        Task<int> Insert<T>(T newEntity) where T : BaseEntity;

        Task Upsert<T>(T entity) where T : BaseEntity;

        Task Delete<T>(T entity) where T : BaseEntity;

        Task<int> InsertRange<T>(ICollection<T> newEntities) where T : BaseEntity;

        Task<int> UpdateRange<T>(ICollection<T> newEntities) where T : BaseEntity;

        void CloseConnection();

        void ResetDatabasePool();
    }
}