using System.Runtime.Serialization;
using MapPointInfo.Domain.Enum;

namespace MapPointInfo.Domain
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class EmbedInfo
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public required string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public EmbedType EmbedType { get; set; }

    }
}