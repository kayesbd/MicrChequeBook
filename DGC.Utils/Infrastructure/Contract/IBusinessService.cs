namespace KBZ.Utils.Infrastructure.Contract
{
    /// <summary>
    /// Interface for all business services
    /// </summary>
    /// <typeparam name="T">The domain type that this business service returns</typeparam>
    public interface IBusinessService<T,U> 
        where T: IBaseDomainModel
    {
        /// <summary>
        /// Retrieve a single item using it's primary key, exception if not found
        /// </summary>
        /// <param name="primaryKey">The primary key of the record</param>
        /// <returns>T</returns>
        T Single(object primaryKey);

        /// <summary>
        /// Retrieve a single item by it's primary key or return null if not found
        /// </summary>
        /// <param name="primaryKey">Prmary key to find</param>
        /// <returns>T</returns>
        T SingleOrDefault(object primaryKey);

        /// <summary>
        /// Returns all the rows for type T
        /// </summary>
        /// <returns></returns>
        //IEnumerable<T> GetAll();

      
        //Pagination<T> GetDataWithPaging(int page, long pagesize, string dbProcedureName, string filter, string sort, Dictionary<string, string> param);
        //Pagination<T> GetDataWithPaging(NameValueCollection parametersValues, string dbProcedureName, Dictionary<string, string> parameters = null);
        //Pagination<T> GetAllByPage(int page, long row, Dictionary<string, object> paramList);
        /// <summary>
        /// Does this item exist by it's primary key
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        bool Exists(object primaryKey);

        /// <summary>
        /// Inserts the data into the table
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        /// <param name="userId">The user performing the insert</param>
        /// <returns></returns>
        long Insert(T entity);

        /// <summary>
        /// Updates this entity in the database using it's primary key
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="userId">The user performing the update</param>
        void Update(T entity);

        /// <summary>
        /// Deletes this entry fro the database
        /// ** WARNING - Most items should be marked inactive and Updated, not deleted
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="userId">The user Id who deleted the entity</param>
        /// <returns></returns>
        void Delete(T entity);
        void Delete(long Id);

        BusinessTransaction<T, U> GetTransaction();
    }
}