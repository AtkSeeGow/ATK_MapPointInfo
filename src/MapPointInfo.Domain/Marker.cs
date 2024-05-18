using System.Runtime.Serialization;

namespace MapPointInfo.Domain
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Marker : DomainBase
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public required string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public required Position Position { get; set; }

        // 縣市

        // 區域
    }
}