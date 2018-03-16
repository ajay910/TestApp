using Ninject;
using Ninject.Modules;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Database;
using TestApp.Repository;
using TestApp.Utilities;

namespace TestApp
{
    public class BusinessDependencies : NinjectModule
    {
        public override void Load()
        {
            Bind<SQLiteConnectionString>().ToMethod(ctx =>
            {
                var pathProvider = ctx.Kernel.Get<IPathProvider>();
                //var finalDbPath = pathProvider.RestoreMasterData(false);
                return new SQLiteConnectionString(pathProvider.MainDatabasePath, false);
            }).InSingletonScope();

            Bind<IDatabase>()
              .To<DatabaseConnection>()
              .InSingletonScope()
              .WithConstructorArgument(typeof(SQLiteConnectionString), c => c.Kernel.Get<SQLiteConnectionString>());

            Bind<IUserRepository>().To<UserRepository>();
        }
    }
}
