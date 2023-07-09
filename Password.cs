namespace PasswordManager
{
    public class Password
    {
        public int ID { get; set; }

        public string? Category { get; set; }

        public string? App { get; set; }

        public string? UserName { get; set; }

        public string? EncryptedPassword { get; set; }
    }
}