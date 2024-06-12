using System.Runtime.Serialization;

namespace MapPointInfo.Domain
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Condition
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime? BeginDate { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool HasVideo { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool HasRouteInfo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Title { get; set; } = string.Empty;
    }
}