namespace WebServer.Configurations
{
    public class JWTOptions
    {
        public const string JWT = "JWT";

        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
