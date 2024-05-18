using System.Runtime.Serialization;

namespace MapPointInfo.Domain
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class MarkerInfo : DomainBase
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
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public required IList<EmbedInfo> EmbedInfos { get; set; }
    }
}