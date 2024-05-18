using System.Runtime.Serialization;

namespace MapPointInfo.Domain
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class Position
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public double Lat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public double Lng { get; set; }
    }
}