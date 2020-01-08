
namespace IoT
{
    public class Credentials
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class Settings
    {
        public static string DB_CONNECTION_STRING = @"Data Source=SQL5041.site4now.net;Initial Catalog=DB_A50EBA_iot191;User Id=DB_A50EBA_iot191_admin;Password=mihaiernest123;";
        public static Credentials LoginCredentials = new Credentials()
        {
            Login = "admin",
            Password = "mihaiernest123"
        };
    }
}