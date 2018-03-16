using TestApp.Database;

namespace TestApp.Repository
{
    internal abstract class BaseRepository
    {
        protected IDatabase Database;

        protected BaseRepository(IDatabase database)
        {
            Database = database;
        }
    }
}