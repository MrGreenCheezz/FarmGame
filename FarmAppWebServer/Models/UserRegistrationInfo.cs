namespace FarmAppWebServer.Models
{
    public class UserRegistrationInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordEncrypted { get; set; }
    }
}
