
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TestApp.Domain;
using TestApp.Utilities;


namespace TestApp.Database
{
   

    public class DatabaseInitializer
    {
        private IDatabase _database;
        private readonly IPathProvider _pathProvider;

        public DatabaseInitializer(IDatabase database, IPathProvider pathProvider)
        {
            _database = database;
            _pathProvider = pathProvider;
        }

        public async Task Execute()
        {
            await CreateAllTables();
        }

        private async Task ClearAllTables()
        {
            var types = GetAllTableTypes();
            foreach (var tableType in types)
                await _database.DeleteAllData(tableType.Name);
        }

      


        private async Task CreateAllTables()
        {
            await _database.CreateTables(GetAllTableTypes());
        }

        private static Type[] GetAllTableTypes()
        {
            var data = typeof(DatabaseConnection).GetTypeInfo();
            return data.Assembly.ExportedTypes.Where(j => j.GetTypeInfo().IsSubclassOf(typeof(BaseEntity))).ToArray();
        }
    }
}