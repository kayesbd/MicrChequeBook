//using System.Data.Objects;
using System.Data.Entity;
using System.Transactions;

//using System.Data.EntityClient;
using KBZ.Utils.Infrastructure.Contract;

namespace KBZ.Utils.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private TransactionScope _transaction;
        protected  DbContext _db;


        public UnitOfWork()
        {

           
          
        }

        public void Dispose()
        {
           
        }

        public void StartTransaction()
        {

            _transaction = new TransactionScope();

               
               

            
        }

        public void Commit()
        {
            _db.SaveChanges();
            _transaction.Complete();
        }

        public DbContext Db
        {
            get { return _db; }
            set { _db = value; }
        }


        
    }
}
