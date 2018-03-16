
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestApp.Domain;

namespace TestApp.Database
{

    public class DatabaseConnection : SQLiteAsyncConnection, IDatabase
    {
        #region Constructors

        public SQLiteConnectionString Connection { get; set; }

        public DatabaseConnection(SQLiteConnectionString connString)
            : base(connString.DatabasePath)
        {
            Connection = connString;
        }

        #endregion Constructors

        #region Public Methods

        public async Task CreateTables(Type[] tableTypes)
        {
            await CreateTablesAsync(CreateFlags.None, tableTypes);
        }

        public async Task<int> DeleteAllData(string tableName)
        {
            var deleteCount = await ExecuteAsync($"delete from {tableName}");
            //await ExecuteAsync("delete from sqlite_sequence where name=@0", tableName);
            return deleteCount;
        }

        public new AsyncTableQuery<T> Table<T>() where T : BaseEntity, new()
        {
            return base.Table<T>();
        }

        public Task<T> Get<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity, new()
        {
            return GetAsync(predicate);
        }

        public Task<int> Insert<T>(T newEntity)
            where T : BaseEntity
        {
            if (string.IsNullOrEmpty(newEntity.Id))
                newEntity.Id = Guid.NewGuid().ToString();

            return InsertAsync(newEntity);
        }

        public Task Upsert<T>(T entity)
            where T : BaseEntity
        {
            if (string.IsNullOrEmpty(entity.Id))
                entity.Id = Guid.NewGuid().ToString();

            return InsertOrReplaceAsync(entity);
        }

        public Task Delete<T>(T entity)
            where T : BaseEntity
        {
            return base.DeleteAsync(entity);
        }

        public Task<int> InsertRange<T>(ICollection<T> newEntities)
            where T : BaseEntity
        {
            foreach (var item in newEntities.Where(j => string.IsNullOrEmpty(j.Id)))
                item.Id = Guid.NewGuid().ToString();

            return InsertAllAsync(newEntities);
        }

        public Task<int> UpdateRange<T>(ICollection<T> newEntities)
            where T : BaseEntity
        {
            return UpdateAllAsync(newEntities);
        }

        public async Task<T> RunInTransaction<T>(Func<DatabaseConnection, Task<T>> taskFunc)
        {
            var connection = GetConnection();
            var savePoint = string.Empty;
            try
            {
                savePoint = connection.SaveTransactionPoint();
                var returnValue = await taskFunc(this);
                //connection.Commit();
                connection.Release(savePoint);
                return returnValue;
            }
            catch (Exception ex)
            {
                connection.RollbackTo(savePoint);
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
        }

        public void CloseConnection()
        {
            GetConnection().Close();
        }

        public void ResetDatabasePool()
        {
            ResetPool();
        }

        #endregion Public Methods

        #region Private Methods

        private new SQLiteConnectionWithLock GetConnection()
        {
            return base.GetConnection();
        }

        #endregion Private Methods

        //#region Internal Methods

        //public async Task<T> Insert<T>(T newEntity, bool recordStatusChanged = true)
        //    where T : BaseEntity
        //{
        //    newEntity.Id = newEntity.Id ?? Guid.NewGuid().ToString();
        //    if (recordStatusChanged)
        //        newEntity.RecordStatus = (int)RecordStatus.New;
        //    await InsertAsync(newEntity);
        //    return newEntity;
        //}

        //internal async Task<T> Upsert<T>(T newEntity, Expression<Func<T, bool>> updateCriertia, bool recordStatusChanged = true)
        //    where T : BaseEntity, new()
        //{
        //    var existingEntity = await this.WhereFirst(updateCriertia);

        //    if (existingEntity == null)
        //    {
        //        return await Insert(newEntity, recordStatusChanged);
        //    }

        //    newEntity.Id = existingEntity.Id;
        //    if (recordStatusChanged)
        //    {
        //        if (newEntity.RecordStatus == null)
        //            newEntity.RecordStatus = (int)RecordStatus.Modify;
        //    }
        //    await UpdateAsync(newEntity);
        //    return newEntity;
        //}

        //internal async Task<bool> TryUpdate<T>(T existingEntity)
        //    where T : BaseEntity, new()
        //{
        //    var existing = await FindAsync<T>(existingEntity.Id);
        //    if (existing == null)
        //        return false;
        //    if (existingEntity.RecordStatus == null)
        //        existingEntity.RecordStatus = (int)RecordStatus.Modify;
        //    var count = await UpdateAsync(existingEntity);
        //    return count == 1;
        //}

        //internal Task<int> UpdateRange<T>(ICollection<T> newEntities, bool recordStatusChanged = true)
        //   where T : BaseEntity
        //{
        //    if (recordStatusChanged)
        //    {
        //        foreach (var item in newEntities)
        //        {
        //            if (item.RecordStatus == null)
        //                item.RecordStatus = (int)RecordStatus.Modify;
        //        }
        //    }

        //    return UpdateAllAsync(newEntities);
        //}

        //internal async Task<int> UpsertRange<T>(ICollection<T> entities, bool recordStatusChanged = true)
        //    where T : BaseEntity, new()
        //{
        //    var needToUpdateIds = await GetExistingEntityIds(entities);

        //    var updateEntities = entities.Where(j => needToUpdateIds.Contains(j.Id)).ToList();
        //    var newEntities = entities.Where(j => !needToUpdateIds.Contains(j.Id)).ToList();

        //    var insertCount = 0;
        //    var updateCount = 0;
        //    if (newEntities.Any())
        //        insertCount = await InsertRange(newEntities, recordStatusChanged);

        //    if (updateEntities.Any())
        //        updateCount = await UpdateRange(updateEntities, recordStatusChanged);

        //    return updateCount + insertCount;
        //}

        //private async Task<List<string>> GetExistingEntityIds<T>(ICollection<T> entities) where T : BaseEntity, new()
        //{
        //    var idExists = entities.Where(i => !string.IsNullOrEmpty(i.Id)).ToList();
        //    var needToUpdateIds = new List<string>();

        //    var checkedRowCount = 0;
        //    while (checkedRowCount != (idExists.Count))
        //    {
        //        int pageRowCount;
        //        if (idExists.Count < 1000)
        //        {
        //            pageRowCount = idExists.Count;
        //        }
        //        else
        //        {
        //            pageRowCount = idExists.Count - checkedRowCount;
        //        }

        //        if (pageRowCount > 1000)
        //        {
        //            pageRowCount = 999;
        //        }

        //        var ids = idExists.GetRange(checkedRowCount, pageRowCount).Select(j => j.Id).ToList();
        //        checkedRowCount += pageRowCount;

        //        needToUpdateIds.AddRange((await Table<T>().Where(j => ids.Contains(j.Id)).ToListAsync()).Select(j => j.Id).ToList());
        //    }

        //    return needToUpdateIds;
        //}

        //internal Task<int> Delete<T>(T newEntities)
        //    where T : BaseEntity
        //{
        //    newEntities.IsDeleted = true;
        //    if (newEntities.RecordStatus == null)
        //        newEntities.RecordStatus = (int)RecordStatus.Delete;
        //    return UpdateAsync(newEntities);
        //}

        //internal Task<int> HardDelete<T>(T entity)
        //    where T : BaseEntity
        //{
        //    return DeleteAsync(entity);
        //}

        //internal async Task<bool> HardDelete<T>(List<T> entities)
        //    where T : BaseEntity
        //{
        //    var deleteTasks = entities.Select(HardDelete);
        //    await Task.WhenAll(deleteTasks);
        //    return true;
        //}

        //internal Task<int> DeleteRange<T>(ICollection<T> newEntities)
        //    where T : BaseEntity, new()
        //{
        //    foreach (var item in newEntities)
        //    {
        //        item.IsDeleted = true;
        //        if (item.RecordStatus == null)
        //            item.RecordStatus = (int)RecordStatus.Delete;
        //    }

        //    return UpdateAllAsync(newEntities);
        //}

        //internal async Task LoadChildrens<TMaster, TChild>
        //  (
        //   IEnumerable<TMaster> masterList,
        //   Expression<Func<TMaster, TChild>> receiverParam,
        //   Func<TMaster, string> masterForeignKey,
        //  Func<TChild, string> childPrimaryKey,
        //  Expression<Func<TChild, bool>> childFilterCondition
        //  ) where TChild : BaseEntity, new()
        //{
        //    var data = await Table<TChild>().Where(childFilterCondition).ToListAsync();
        //    masterList.Map(data, masterForeignKey, childPrimaryKey, receiverParam);
        //}

        //internal async Task LoadChildrens<TMaster, TChild>
        //  (
        //  IEnumerable<TMaster> masterList,
        //  Expression<Func<TMaster, ICollection<TChild>>> receiverParam,
        //  Func<TMaster, string> masterForeignKey,
        //  Func<TChild, string> childPrimaryKey,
        //  Expression<Func<TChild, bool>> childFilterCondition
        //  ) where TChild : BaseEntity, new()
        //{
        //    var data = await Table<TChild>().Where(childFilterCondition).ToListAsync();
        //    masterList.Map(data, masterForeignKey, childPrimaryKey, receiverParam);
        //}

        //internal async Task LoadChildrens<TMaster, TChild>
        //  (
        //   IEnumerable<TMaster> masterList,
        //   Expression<Func<TMaster, TChild>> receiverParam,
        //   Func<TMaster, string> masterForeignKey,
        //  Func<TChild, string> childPrimaryKey
        //  ) where TChild : BaseEntity, new()
        //{
        //    var list = masterList.Select(masterForeignKey).Distinct();
        //    var data = await Table<TChild>().Where(i => list.Contains(i.Id)).ToListAsync();
        //    masterList.Map(data, masterForeignKey, childPrimaryKey, receiverParam);
        //}

        //internal async Task UpdateOnlyVersion<T>(string id, byte[] version, string fieldName = nameof(BaseEntity.Id)) where T : BaseEntity
        //{
        //    var stringVersion = version.ByteArrayToString();
        //    stringVersion = string.IsNullOrEmpty(stringVersion) ? "null" : $"'{stringVersion}'";
        //    var updateQuery = $"UPDATE {typeof(T).Name} SET RecordStatus = null, Version = {stringVersion} WHERE {fieldName} = '{id}'";
        //    await ExecuteAsync(updateQuery);
        //}

        //internal async Task UpdateIdAndVersion<T>(Guid id, byte[] version, Guid? newId = null) where T : BaseEntity
        //{
        //    var fieldName = nameof(BaseEntity.Id);
        //    if (!newId.HasValue)
        //        newId = id;
        //    var stringVersion = version.ByteArrayToString();
        //    stringVersion = string.IsNullOrEmpty(stringVersion) ? "null" : $"'{stringVersion}'";
        //    var updateQuery = $"UPDATE {typeof(T).Name} SET {fieldName} = '{newId}', {nameof(BaseEntity.RecordStatus)} = null, {nameof(BaseEntity.Version)} = {stringVersion} WHERE {fieldName} = '{id}'";
        //    await ExecuteAsync(updateQuery);
        //}

        //internal async Task UpdateRef<T>(Guid id, string fieldName, Guid? newId) where T : BaseEntity
        //{
        //    if (!newId.HasValue)
        //        newId = id;
        //    var updateQuery = $"UPDATE {typeof(T).Name} SET {fieldName} = '{newId}' WHERE {fieldName} = '{id}'";
        //    await ExecuteAsync(updateQuery);
        //}

        //#endregion Internal Methods
    }
}