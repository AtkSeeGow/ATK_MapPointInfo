namespace MapPointInfo.Domain.Options
{
    public class AuthorizationOption
    {
        public bool IsUseActiveDirectory { get; set; }
        public int ActiveDirectoryContextType { get; set; }
        public required string ActiveDirectoryName { get; set; }
        public required string ActiveDirectoryUserName { get; set; }
        public required string ActiveDirectoryPassword { get; set; }
        public bool IsUseRSAEncoder { get; set; }
    }
}