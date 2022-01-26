using System;
using System.Threading;
using AutoMapper;
using KBZ.Utils.Infrastructure.Contract;

namespace KBZ.Utils.Infrastructure
{
    public class NonAuditableBaseService<T,U> : IBusinessService<T,U> 
        where T : IBaseDomainModel
    {
        private readonly IBaseRepository<U> _dbService;
        public AutoMapper.Mapper map;
        public NonAuditableBaseService(IBaseRepository<U> dbService)
        {
            if (dbService == null) throw new ArgumentNullException("dbService");
            _dbService = dbService;
        }

        public T Single(object primaryKey)
        {
            return map.Map<T>(_dbService.Single(primaryKey)); 
        }

        public T SingleOrDefault(object primaryKey)
        {
            return map.Map<T>(_dbService.SingleOrDefault(primaryKey));
        }

        //public virtual IEnumerable<T> GetAll()
        //{
        //    var data = _dbService.GetAll();
        //    return Mapper.Map<IEnumerable<T>>(data);
        //}

        //public Pagination<T> GetDataWithPaging(int page, long pagesize, string dbProcedureName, string filter, string sort, Dictionary<string, string> param)
        //{
        //    Pagination<U> list = _dbService.GetDataWithPaging(page, pagesize, dbProcedureName, filter, sort, param);
        //    Pagination<T> pagingList = new Pagination<T>();
        //    pagingList.total = list.total;
        //    pagingList.rows = Mapper.Map<List<T>>(list.rows);
        //    return pagingList;
        //}

        //public Pagination<T> GetDataWithPaging(NameValueCollection parametersValues, string dbProcedureName, Dictionary<string, string> parameters = null)
        //{
        //    Pagination<U> list = _dbService.GetDataWithPaging(parametersValues, dbProcedureName,parameters);
        //    Pagination<T> pagingList = new Pagination<T>();
        //    pagingList.total = list.total;
        //    pagingList.rows = Mapper.Map<List<T>>(list.rows);
        //    return pagingList;
        //}

        //public Pagination<T> GetAllByPage(int page, long itemsPerPage, Dictionary<string, object> paramList)
        //{
        //    Pagination<U> list = _dbService.GetAllWithPaging(page, itemsPerPage, paramList);
        //    Pagination<T> pagingList = new Pagination<T>();
        //    pagingList.total = list.total;
        //    pagingList.rows = Mapper.Map<List<T>>(list.rows);
        //    return pagingList;
        //}

        public bool Exists(object primaryKey)
        {
            return _dbService.Exists(primaryKey);
        }

        public long CurrentPersonId
        {
            get
            {
                CustomUserPrincipal principal = Thread.CurrentPrincipal as CustomUserPrincipal;
                if (principal == null)
                    return 0;
                return principal.PersonId;
            }
        }
        public CustomUserPrincipal CurrentUser
        {
            get
            {
                CustomUserPrincipal principal = Thread.CurrentPrincipal as CustomUserPrincipal;
                return principal;
            }

        }
        public long CurrentUserId
        {
            get
            {
                CustomUserPrincipal principal = Thread.CurrentPrincipal as CustomUserPrincipal;
                return principal.Id;
            }
        }
        public virtual long Insert(T entity)
        {

            // TODO : Must ensure all our primary keys are the same type            

            var dbEntity = map.Map<U>(entity);
            //if (CurrentPersonId == null)
            //{
            //    dynamic dynEntity = dbEntity as dynamic;
            //    CurrentPersonId=dynEntity.
            //}

            var returnedId = _dbService.Insert(dbEntity, CurrentPersonId);

            return returnedId;

            //throw new NullReferenceException("Could not cast the database Id to a recognised type");
        }

        public virtual void Update(T entity)
        {
            var dbEntity = map.Map<U>(entity);
            _dbService.Update(dbEntity, CurrentPersonId);
        }

        public virtual void Delete(T entity)
        {
            var dbEntity = map.Map<U>(entity);
            _dbService.Delete(dbEntity, CurrentPersonId);
        }


        private static BusinessTransaction<T,U> _transaction;
        public BusinessTransaction<T,U> GetTransaction()
        {
            if (_transaction == null)
                _transaction = new BusinessTransaction<T,U>(_dbService); //new BusinessTransaction<T>(_dbService);
            return _transaction;
        }





        public void Delete(long Id)
        {
            U dbEntity = _dbService.SingleOrDefault(Id);
            if (dbEntity != null)
            {
                _dbService.Delete(dbEntity, CurrentPersonId);
            }
        }
    }
}
