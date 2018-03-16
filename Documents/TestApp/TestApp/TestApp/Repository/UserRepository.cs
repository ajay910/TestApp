using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Database;
using TestApp.Domain;

namespace TestApp.Repository
{
    internal class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IDatabase database) : base(database)
        {
        }

        public Task<List<Users>> GetAll()
        {
            return Database.Table<Users>().OrderBy(i => i.SortOrder).ToListAsync();
        }

        public Task<Users> GetById(string id)
        {
            return Database.Table<Users>().Where(k => k.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> IsEmailExist(string id, string email)
        {
            return (await Database.Table<Users>().ToListAsync()).Any(k => k.Id != id && k.Email.ToLower() == email.ToLower());
        }

        public async Task InsertOrUpdate(Users user)
        {
            if(user.SortOrder == 0)
            {
                user.SortOrder = (await GetAll()).Select(j => j.SortOrder).LastOrDefault() + 1;
            }
            await Database.Upsert(user);
        }

        public async Task Delete(string id)
        {
            await Database.Delete(await GetById(id));
        }
    }
}