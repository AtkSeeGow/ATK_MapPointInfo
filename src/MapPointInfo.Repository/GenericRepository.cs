using MapPointInfo.Domain.Interface;
using MapPointInfo.Domain.Options;
using MapPointInfo.Repository.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace MapPointInfo.Repository
{
    public class GenericRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IIdentifier<TKey> where TKey : IEquatable<TKey>
    {
        protected IMongoCollection<BsonDocument> BsonDocumentCollection
        {
            get { return bsonDocumentCollection; }
        }
        private IMongoCollection<BsonDocument> bsonDocumentCollection;

        public IMongoCollection<TEntity> TEntityCollection
        {
            get { return tEntityCollection; }
        }
        private IMongoCollection<TEntity> tEntityCollection;

        #region constructors

        public GenericRepository(MongoDBOptions mongoDBOptions)
        {
            var mongoUrl = new MongoUrl(mongoDBOptions.ConnectionString + mongoDBOptions.CollectionName);
            var client = new MongoClient(mongoUrl);
            this.bsonDocumentCollection = client.GetDatabase(mongoUrl.DatabaseName).GetCollection<BsonDocument>(getCollectionName());
            this.tEntityCollection = client.GetDatabase(mongoUrl.DatabaseName).GetCollection<TEntity>(getCollectionName());
        }

        #endregion

        #region public methods

        public TEntity Create(TEntity entity)
        {
            this.tEntityCollection.InsertOne(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            this.tEntityCollection.ReplaceOne<TEntity>(e => entity.Id.Equals(e.Id), entity);
            return entity;
        }

        public virtual async Task CreateAll(IEnumerable<TEntity> entities)
        {
            await TEntityCollection.InsertManyAsync(entities);
        }

        public virtual async Task<TEntity> FindBy(object id)
        {
            return await this.tEntityCollection.Find<TEntity>(e => e.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await this.tEntityCollection.ReplaceOneAsync<TEntity>(e => entity.Id.Equals(e.Id), entity);
            return entity;
        }

        public virtual async Task Delete(object id)
        {
            await this.tEntityCollection.DeleteOneAsync<TEntity>(e => e.Id.Equals(id));
        }

        public virtual bool Exist(object id)
        {
            return this.TEntityCollection.CountDocuments(e => e.Id.Equals(id)) > 0;
        }

        public virtual async Task<IEnumerable<TEntity>> FetchAll()
        {
            return await TEntityCollection.Find(new BsonDocument()).ToListAsync();
        }

        public virtual async Task<long> DeleteAll()
        {
            var task = await TEntityCollection.DeleteManyAsync(new BsonDocument());
            return task.DeletedCount;
        }

        public virtual async Task<IEnumerable<TEntity>> FindBy(Expression<Func<TEntity, bool>> filter)
        {
            return await this.tEntityCollection.Find<TEntity>(filter).ToListAsync();
        }

        #endregion

        #region IEnumerable<TEntity> Members

        public IEnumerator<TEntity> GetEnumerator()
        {
            return this.tEntityCollection.AsQueryable().GetEnumerator();
        }

        #endregion

        #region IQueryable Members

        public Type ElementType
        {
            get { return this.tEntityCollection.AsQueryable().ElementType; }
        }

        public Expression Expression
        {
            get { return this.tEntityCollection.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return this.tEntityCollection.AsQueryable().Provider; }
        }

        #endregion

        private string getCollectionName()
        {
            return typeof(TEntity).Name;
        }
    }

}
