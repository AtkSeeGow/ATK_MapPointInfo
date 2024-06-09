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
        public bool MustVideo { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public bool MustRouteInfo { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Ttitle { get; set; } = string.Empty
    }
}