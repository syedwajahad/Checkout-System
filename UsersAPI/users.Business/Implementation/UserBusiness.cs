using System.Net;
using users.Business.Interface;
using users.DataAccess.Interface;
using users.DataObjects.DataModels;

namespace users.Business.Implementation
{
    public class UsersBusiness : IUserBusiness
    {

        private readonly IUserDataAccess _UsersDataAccess;
        public UsersBusiness(IUserDataAccess UserDataAccess)
        {
            _UsersDataAccess = UserDataAccess;
        }
        //Todo: negative conditions should come first
        //TODO: need to add try catch
        /// <summary>
        /// Creates a new user by calling the CreateUser method from the _UsersDataAccess class.
        /// </summary>
        /// <param name="user">The user object containing information about the new user.</param>
        /// <returns>The created Users object representing the newly created user.</returns>
        public async Task<ApiResponse<User>> CreateUser(User user)
        {
            try
            {
                var output = await _UsersDataAccess.CreateUser(user);
                var response = new ApiResponse<User>();
                if (output == null)
                {
                    response.Status = HttpStatusCode.InternalServerError;
                    response.Data = null;
                    response.Message = "InternalServerError";
                }
                else
                {
                    response.Status = HttpStatusCode.OK;
                    response.Data = output;
                    response.Message = " User created successfully";
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Deletes a user with the specified UserId.
        /// </summary>
        /// <param name="UserId">The ID of the user to be deleted.</param>
        /// <returns>An integer indicating the result of the deletion operation.</returns>
        public async Task<ApiResponse<int>> DeleteUsers(int UserId)
        {
            try
            {
                var output = await _UsersDataAccess.DeleteUsers(UserId);
                var response = new ApiResponse<int>();
                if (output == 0)
                {
                    response.Status = HttpStatusCode.NoContent;
           
                    response.Message = "No users found";
                }
                else
                {
                    response.Status = HttpStatusCode.OK;
                    response.Message = " User deleted successfully";
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Retrieves a list of users asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns a list of users.</returns>
        public async Task<ApiResponse<List<User>>> GetAllUsers()
        {
        
            try
            {
                var response = new ApiResponse<List<User>>();
                var output = await _UsersDataAccess.GetAllUsers();
               
                if (output == null)
                {
                    response.Status = HttpStatusCode.NoContent;
                    response.Data = null;
                    response.Message = "Users not found";
                }
                else
                {
                    response.Status = HttpStatusCode.OK;
                    response.Data = output;
                    response.Message = "Retrived users successfully";
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        // <summary>
        /// Retrieves user information by user ID asynchronously.
        /// </summary>
        /// <param name="UserId">The ID of the user to retrieve.</param>
        /// <returns>A Task representing the asynchronous operation with the retrieved Users object.</returns>
        public async Task<ApiResponse<User>> GetUsersById(int UserId)
        {
            try
            {
                var output = await _UsersDataAccess.GetUsersById(UserId);
                var response = new ApiResponse<User>();
                if (output == null)
                {
                    response.Status = HttpStatusCode.NoContent;
                    response.Data = null;
                    response.Message = "User not found";
                }
                else
                {
                    response.Status = HttpStatusCode.OK;
                    response.Data = output;
                    response.Message = "Retrived user successfully";
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Updates user information.
        /// </summary>
        /// <param name="user">The Users object containing updated user information.</param>
        /// <param name="UserId">The ID of the user to be updated.</param>
        /// <returns>A Task representing the asynchronous operation with the updated Users object.</returns>
        public async Task<ApiResponse<User>> UpdateUsers(User user, int UserId)
        {
            try
            {
                var output = await _UsersDataAccess.UpdateUsers(user, UserId);
                var response = new ApiResponse<User>();
                if (output == null)
                {
                    response.Status = HttpStatusCode.NoContent;
                    response.Data = null;
                    response.Message = "User not found";
                }
                else
                {
                    response.Status = HttpStatusCode.OK;
                    response.Data = output;
                    response.Message = "User updated successfully";
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
