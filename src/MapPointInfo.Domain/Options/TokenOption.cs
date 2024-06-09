namespace MapPointInfo.Domain.Options
{
    public class TokenOption
    {
        public required string AllowedIssuer { get; set; }
        public required string AllowedAudience { get; set; }
        public required string SymmetricKey { get; set; }
    }
}