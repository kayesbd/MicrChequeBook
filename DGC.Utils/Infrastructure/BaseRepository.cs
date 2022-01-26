using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using KBZ.Utils.Infrastructure.Contract;

namespace KBZ.Utils.Infrastructure
{
    /// <summary>
    /// Base class for all SQL based service classes
    /// </summary>
    /// <typeparam name="T">The domain object type</typeparam>
    /// <typeparam name="TT">The database object type</typeparam>
    public class BaseRepository<T, TT> : IBaseRepository<T>
        where TT : DbContext, new()
        where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly TT context = new TT();
        internal DbSet<T> dbSet;
        public BaseRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            _unitOfWork = unitOfWork;
            _unitOfWork.Db = (DbContext)context;
            this.dbSet = _unitOfWork.Db.Set<T>();
        }

        /// <summary>
        /// Returns the object with the primary key specifies or throws
        /// </summary>
        /// <typeparam name="TU">The type to map the result to</typeparam>
        /// <param name="primaryKey">The primary key</param>
        /// <returns>The result mapped to the specified type</returns>
        public T Single(object primaryKey)
        {

            var dbResult = _unitOfWork.Db.Set<T>().Find(primaryKey);
            return dbResult;

        }

        /// <summary>
        /// Returns the object with the primary key specifies or the default for the type
        /// </summary>
        /// <typeparam name="TU">The type to map the result to</typeparam>
        /// <param name="primaryKey">The primary key</param>
        /// <returns>The result mapped to the specified type</returns>
        public T SingleOrDefault(object primaryKey)
        {
            var dbResult = dbSet.Find(primaryKey);
            return dbResult;
        }

        //public List<T> GetList()
        //{
        //    var iterator = this.GetAll().AsEnumerable<T>().ToList();
        //    return iterator;
        //}

        //public virtual IEnumerable<T> GetAllWithDeleted()
        //{
        //    var dbresult = _unitOfWork.Db.Fetch<T>("");

        //    return dbresult;
        //}

        public bool Exists(object primaryKey)
        {
            return dbSet.Find(primaryKey) == null ? false : true;
        }

        public virtual long Insert(T entity, long personId)
        {

            dynamic obj = _unitOfWork.Db.Set<T>().Add(entity);
            try
            {
                this._unitOfWork.Db.SaveChanges();
                return obj.Id;

            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                throw;
            }

            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
          

        }

        public virtual void Update(T entity, long personId)
        {
            dbSet.Attach(entity);
            _unitOfWork.Db.Entry(entity).State = EntityState.Modified;
            this._unitOfWork.Db.SaveChanges();
        }
        //public  void AddOrUpdate(T entity, long personId)
        //{

        //    _unitOfWork.Db.Entry(entity).State = EntityState.Modified;
        //    this._unitOfWork.Db.SaveChanges();
        //}

        public virtual long Delete(T entity, long personId)
        {
            if (_unitOfWork.Db.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dynamic obj = dbSet.Remove(entity);
            this._unitOfWork.Db.SaveChanges();
            return obj.Id;
        }

        public IUnitOfWork UnitOfWork { get { return _unitOfWork; } }

        internal DbContext Database { get { return _unitOfWork.Db; } }

        public Dictionary<string, string> GetAuditNames(dynamic dynamicObject)
        {
            throw new NotImplementedException();
        }

        //public virtual IEnumerable<T> GetAll()
        //{
        //    return _unitOfWork.Db.Set<T>().AsEnumerable().ToList();
        //}
    }
}
