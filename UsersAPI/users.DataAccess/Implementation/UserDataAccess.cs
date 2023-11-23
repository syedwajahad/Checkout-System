using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using users.DataAccess.Interface;
using users.DataObjects.DataModels;

namespace users.DataAccess.implementation
{
    public class UserDataAccess : IUserDataAccess
    {
        private readonly IConfiguration _config;
        public readonly SqlConnection _connection;
        public UserDataAccess(IConfiguration config)
        {
            _config = config;
            _connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }
        /// <summary>
        /// Creates a new user asynchronously or returns an existing user if the email already exists.
        /// </summary>
        /// <param name="user">The Users object representing the new user to be created.</param>
        /// <returns>
        /// If the user with the provided email already exists, returns the existing user.
        /// Otherwise, creates a new user with the specified details and returns the created user.
        /// </returns>
        public async Task<User> CreateUser(User user)
        {
            try
            {
                var obj = new
                {
                    name = user.UserName,
                    email = user.UserEmail,
                    role = user.UserRole
                };
                //Todo: negative conditions should come first
                var existingUser = await _connection.QueryFirstOrDefaultAsync<User>(Queries.CheckUsers, new { email = user.UserEmail });
                if (existingUser != null)
                {
                    throw new Exception("User already exists");
                }
                else
                {
                    var id = await _connection.QueryFirstAsync<int>(Queries.AddUsers, obj);
                    user.UserId = id;
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception message : {ex.Message}");
            }
        }
        /// <summary>
        /// Retrieves a list of all users asynchronously.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation with a List of Users.</returns>
        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                var users = await _connection.QueryAsync<User>(Queries.GetAllUsers);
                return (List<User>)users;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception message : {ex.Message}");
            }
        }
        /// <summary>
        /// Retrieves user information by user ID asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>
        /// A Task representing the asynchronous operation with the retrieved Users object.
        /// If the user is found, returns the user information; otherwise, returns null.
        /// </returns>
        public async Task<User> GetUsersById(int userId)
        {
            try
            {
                var user = await _connection.QueryFirstOrDefaultAsync<User>(Queries.GetUser, new { userId });
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception message : {ex.Message}");
            }
        }
        /// <summary>
        /// Updates the user role asynchronously based on the provided Users object and user ID.
        /// </summary>
        /// <param name="user">The Users object containing the updated user information, including the new user role.</param>
        /// <param name="userId">The ID of the user to be updated.</param>
        /// <returns>
        /// A Task representing the asynchronous operation with the updated Users object.
        /// If the user is not found, returns null.
        /// </returns>
        public async Task<User> UpdateUsers(User user, int userId)
        {
            try
            {
                var isUserPresent = await _connection.QueryFirstAsync<User>(Queries.checkUser, new {UserId =  userId});
                if(isUserPresent != null)
                {
                    var obj = new
                    {
                        role = user.UserRole
                    };

                    var users = await _connection.QueryFirstOrDefaultAsync<User>(Queries.ChangeUser, new { userId, role = user.UserRole });
                    return user;
                }
                else
                {
                    throw new Exception("user not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception message : {ex.Message}");
            }
        }
        /// <summary>
        /// Deletes a user asynchronously based on the provided user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to be deleted.</param>
        /// <returns>A Task representing the asynchronous operation with the number of affected rows.</returns>
        public async Task<int> DeleteUsers(int userId)
        {
            try
            {
                var res = await _connection.ExecuteAsync(Queries.RemoveUsers, new { userId });
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception message : {ex.Message}");
            }
        }
    }
}
