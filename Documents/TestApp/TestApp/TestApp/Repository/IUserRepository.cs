using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApp.Domain;

namespace TestApp.Repository
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAll();

        Task<Users> GetById(string id);

        Task InsertOrUpdate(Users user);

        Task Delete(string Id);

        Task<bool> IsEmailExist(string id, string email);
    }
}
