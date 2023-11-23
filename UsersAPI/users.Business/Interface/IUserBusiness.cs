using users.DataObjects.DataModels;

namespace users.Business.Interface
{
    public  interface IUserBusiness
    {
        public Task<ApiResponse<User>> CreateUser(User user);
        public Task<ApiResponse<List<User>>> GetAllUsers();
        public Task<ApiResponse<User>> GetUsersById(int UserId);
        public Task<ApiResponse<User>> UpdateUsers(User user, int UserId);
        public Task<ApiResponse<int>> DeleteUsers(int UserId);       
    }
}
