namespace MapPointInfo.Domain.Options
{
    /// <summary>
    /// 
    /// </summary>
    public class MongoDBOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public required string ConnectionString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public required string CollectionName { get; set; }
    }
}