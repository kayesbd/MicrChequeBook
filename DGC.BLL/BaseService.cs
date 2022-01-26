using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using AutoMapper;

using KBZ.Utils.Infrastructure;
using KBZ.Utils.Infrastructure.Contract;

namespace KBZ.BLL
{
    public class BaseService<T,U> : IBusinessService<T,U> 
        where T : IDomainModel 
    {
        private readonly IBaseRepository<U> _dbService;
        public AutoMapper.Mapper map;
        public BaseService(IBaseRepository<U> dbService)
        {
            if (dbService == null) throw new ArgumentNullException("dbService");
            _dbService = dbService;
        }

        public T Single(object primaryKey)
        {
            
            dynamic domainModel= map.Map<T>(_dbService.Single(primaryKey));
            Dictionary<string, string> auditableNames = _dbService.GetAuditNames(domainModel);
            domainModel.CreatedByName = auditableNames["CreatedBy"].ToString();
            domainModel.ModifiedByName = auditableNames["ModifiedBy"].ToString();
            return domainModel;
        }

        public T SingleOrDefault(object primaryKey)
        {
            return map.Map<T>(_dbService.SingleOrDefault(primaryKey));
        }

        //public virtual IEnumerable<T> GetAll()
        //{
        //    return Mapper.Map<IEnumerable<T>>(_dbService.GetAll());
        //}

        //public Pagination<T> GetDataWithPaging(int page, long pagesize, string dbProcedureName, string filter, string sort, Dictionary<string, string> param)
        //{
        //    Pagination<U> list = _dbService.GetDataWithPaging(page, pagesize, dbProcedureName, filter, sort,param);
        //    Pagination<T> pagingList = new Pagination<T>();
        //    pagingList.total = list.total;
        //    pagingList.rows = Mapper.Map<List<T>>(list.rows);
        //    return pagingList;
        //}

        

        //public Pagination<T> GetDataWithPaging(NameValueCollection parametersValues, string dbProcedureName, Dictionary<string, string> parameters = null)
        //{
        //    Pagination<U> list = _dbService.GetDataWithPaging(parametersValues, dbProcedureName, parameters);
        //    Pagination<T> pagingList = new Pagination<T>();
        //    pagingList.total = list.total;
        //    pagingList.rows = Mapper.Map<List<T>>(list.rows);
        //    return pagingList;
        //}

        /// <summary>
        ///  Returns a list of objects for a list of a certain page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        //public Pagination<T> GetAllByPage(int page, long itemsPerPage, Dictionary<string, object> paramList)
        //{
        //    Pagination<U> list = _dbService.GetAllWithPaging(page, itemsPerPage, paramList);
        //    Pagination<T> pagingList = new Pagination<T>();
        //    pagingList.total = list.total;
        //    pagingList.rows = Mapper.Map<List<T>>(list.rows);
        //    return pagingList;
        //}

        //public IList<DropDownSelectListItem> GetDropDownData(string valueColumn, string displayColumn, string orderByColumnName = "", Dictionary<string, object> paramList = null)
        //{
        //    return _dbService.GetDropDownData(valueColumn, displayColumn, orderByColumnName, paramList);
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
                if(principal ==null)
                {
                    if (HttpContext.Current.User != null)
                    {
                        principal = HttpContext.Current.User as CustomUserPrincipal;
                    }
                }
                if (principal == null)
                {
                    return 0;
                }

                return principal.PersonId;
            }
        }

    
        public long CurrentOrganizationId
        {
            get
            {
                CustomUserPrincipal principal = Thread.CurrentPrincipal as CustomUserPrincipal;
                return principal.OrganizationId;
            }
        }

        public virtual long Insert(T entity)
        {
            entity.SetCreateProperties(CurrentPersonId);
            entity.SetUpdateProperties(CurrentPersonId);

            var dbEntity = map.Map<U>(entity);

            var returnedId = _dbService.Insert(dbEntity, CurrentPersonId);
            return returnedId;
        }

        public virtual void Update(T entity)
        {
            entity.SetUpdateProperties(CurrentPersonId);
            var dbEntity = map.Map<U>(entity);
            _dbService.Update(dbEntity, CurrentPersonId);
        }

        public virtual void Delete(Guid id)
        {
            T entity = map.Map<T>(_dbService.SingleOrDefault(id));
            if (entity != null)
            {
                entity.MarkDeleted(CurrentPersonId);
                var dbEntity = map.Map<U>(entity);
                _dbService.Update(dbEntity, CurrentPersonId);
            }
        }

        public virtual void Delete(T entity)
        {
            entity.MarkDeleted(CurrentPersonId);
            //Update(entity);

            var dbEntity = map.Map<U>(entity);
            _dbService.Update(dbEntity, CurrentPersonId);
        }

        private static BusinessTransaction<T,U> _transaction;
        public BusinessTransaction<T,U> GetTransaction()
        {
            if (_transaction == null)
                _transaction = new BusinessTransaction<T, U>(_dbService); //new BusinessTransaction<T>(_dbService);
            else
            {
                if (!_transaction.IsInTransaction)
                    _transaction.StartTransaction();
            }
            return _transaction;
        }


        public virtual void Delete(long Id)
        {
            throw new NotImplementedException();
        }

       
    }
}
