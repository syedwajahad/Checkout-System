using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using users.DataObjects.DataModels;

namespace users.DataAccess.Interface
{
    public  interface IUserDataAccess
    {
        public Task<User> CreateUser(User user);
        public Task<List<User>> GetAllUsers();  
        public Task<User> GetUsersById(int userId);
        public Task<User> UpdateUsers(User user,int userId);
        public Task<int> DeleteUsers(int userId);
    }
}
