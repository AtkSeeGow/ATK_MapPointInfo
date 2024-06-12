using MapPointInfo.Domain;
using MapPointInfo.Domain.Options;

namespace MapPointInfo.Repository
{
    public class MarkerRepository : GenericRepository<Marker, Guid>
    {
        public MarkerRepository(MongoOption mongoOption) : base(mongoOption)
        {
        }


    }
}
