namespace users.DataObjects.DataModels
{
    public  class Queries
    {
        public static string AddUsers = $"INSERT INTO Users ( UserName, UserEmail,UserRole) VALUES (@name, @email, @role)" + "SELECT SCOPE_IDENTITY()";
        public static string CheckUsers = $"select * from Users where UserEmail = @email";
        public static string GetAllUsers = "select * from Users";
        public static string GetUser = $"select * from Users Where UserId = @UserId";
        public static string ChangeUser = $"UPDATE Users set UserRole= @role where UserId = @UserId";
        public static string RemoveUsers= $"DELETE FROM Users  where UserId = @UserId";

        public static string checkUser = $"select * from users where userId = @UserId";

    }
}
