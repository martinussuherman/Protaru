namespace Protaru.Helper
{
    public class SmtpOptions
    {
        public const string OptionsName = "Smtp";

        public string From { get; set; }
        public string Host { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Port { get; set; } = 587; // default smtp port
        public bool UseSSL { get; set; } = true;
    }
}