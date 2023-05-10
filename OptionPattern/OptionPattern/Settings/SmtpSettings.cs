namespace OptionPattern.Settings
{
    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public Credential Credential { get; set; }
        public bool UseDefaultCredential { get; set; }
    }
}
