namespace MapPointInfo.Domain.Interface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TIdType"></typeparam>
    public interface IIdentifier<TIdType> where TIdType : IEquatable<TIdType>
    {
        /// <summary>
        /// 
        /// </summary>
        TIdType Id
        {
            get;
        }
    }
}