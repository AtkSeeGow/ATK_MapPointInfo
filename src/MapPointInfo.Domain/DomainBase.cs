using MapPointInfo.Domain.Interface;
using System.Runtime.Serialization;

namespace MapPointInfo.Domain
{
    [DataContract]
    public partial class DomainBase : IIdentifier<Guid>
    {
        [DataMember]
        public Guid Id { get; set; }
    }
}
