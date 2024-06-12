using MapPointInfo.Domain;
using MapPointInfo.Domain.Options;
using MongoDB.Driver;

namespace MapPointInfo.Repository
{
    public class MarkerInfoRepository : GenericRepository<MarkerInfo, Guid>
    {
        public MarkerInfoRepository(MongoOption mongoOption) : base(mongoOption)
        {
        }

        public IEnumerable<MarkerInfo> FetchBy(Condition condition)
        {
            var builder = Builders<MarkerInfo>.Filter;

            var filter = builder.Where(item => true);

            if (!string.IsNullOrEmpty(condition.Title))
                filter = filter & builder.Where(item => item.Title.Contains(condition.Title));

            if (condition.BeginDate.HasValue)
                filter = filter & builder.Where(item => item.DateTime >= condition.BeginDate.Value);

            if (condition.EndDate.HasValue)
                filter = filter & builder.Where(item => item.DateTime <= condition.EndDate.Value);

            if(condition.HasVideo)
                filter = filter & builder.Where(item => item.EmbedInfos.Any(item => item.EmbedType == Domain.Enum.EmbedType.Youtube));

            if(condition.HasRouteInfo)
                filter = filter & builder.Where(item => item.EmbedInfos.Any(item => item.EmbedType == Domain.Enum.EmbedType.Garmin));

            return this.TEntityCollection.Find(filter).SortByDescending(item => item.Title).ToList();
        }
    }
}
