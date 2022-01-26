using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using KBZ.Utils.Infrastructure;
using KBZ.Utils.Infrastructure.Contract;
using System;
using System.Data.Entity.Validation;
using System.Text;


namespace KBZ.DAL.BankAdmin.Repository
{
    public partial interface IUserInformationRepository 
    {


        UserInformation GetUserByUserId(long userId);
        UserInformation GetUserByUserName(string userName);
        
        UserInformation GetUserByEmail(string email);

        List<UserInformation> GetUserList();








        bool IsUsernameExist(string username);
        bool IsOwnUsernameProvided(string username, long id);
 
        UserInformation InsertUser(UserInformation userInformation);
        void UpdateUser(UserInformation userInformation);
        void UpdateUserAsync(UserInformation userInformation);
        void UpdateUserPassword(UserInformation userInformation);
        void DeleteUser(long userId);

        void UpdateSessionForUser(long userId, DateTime? sessionTimeout);
        bool UpdateStatus(long userId, string status);
    
        UserInformation GetUserForForgotPassword(long userId, string username, string userVerificationCode);
       // bool UpdateUserVerificationCode(UserInformation userInformation);
        UserInformation SelfRegistrationNewCustomer(UserInformation userInfo);
      //  bool RequisitionToggleStatus(long id);
    }

    public class UserInformationRepository :  IUserInformationRepository
    {
        private DCBankAdminContext _dbContextSecurity = new DCBankAdminContext();
       

        public UserInformationRepository()
        {

        }
        public List<UserInformation> GetUserList()
        {
            return _dbContextSecurity.UserInformations.ToList();
        }
        public bool IsUsernameExist(string username)
        {
            var tableName = "[Security].UserInformation";
            var fieldName = "UserName";
            return _dbContextSecurity.Database.SqlQuery<int>("exec [Common].USP_GetCountByFieldAndValue '" + tableName + "','" + fieldName + "','" + username + "'").SingleOrDefault() > 0;
        }

        public bool IsOwnUsernameProvided(string username, long id)
        {
            return _dbContextSecurity.Database.SqlQuery<int>("exec [Security].USP_IsOwnUsernameProvided '" + username + "','" + id + "'").SingleOrDefault() > 0;
        }

        
        public UserInformation InsertUser(UserInformation userInformation)
        {
            var user = _dbContextSecurity.UserInformations.Add(userInformation);
            _dbContextSecurity.SaveChanges();
            return user;
        }

        public void UpdateUser(UserInformation userInformation)
        {
            _dbContextSecurity.Entry(userInformation).State = EntityState.Modified;
            _dbContextSecurity.Entry(userInformation).Property(x => x.Password).IsModified = false;
            _dbContextSecurity.SaveChanges();
        }

        public void DeleteUser(long userId)
        {
            var userInformation = _dbContextSecurity.UserInformations.FirstOrDefault(w => w.Id == userId);
            if (userInformation != null)
            {
                userInformation.IsDeleted = true;
                _dbContextSecurity.UserInformations.Attach(userInformation);
                _dbContextSecurity.Entry(userInformation).Property(x => x.IsDeleted).IsModified = true;
            }
            _dbContextSecurity.SaveChanges();
        }

       

        public async void UpdateUserAsync(UserInformation userInformation)
        {
            _dbContextSecurity.Entry(userInformation).State = EntityState.Modified;
            _dbContextSecurity.Entry(userInformation).Property(x => x.Password).IsModified = false;
            await _dbContextSecurity.SaveChangesAsync();
        }

        
        public void UpdateSessionForUser(long userId, DateTime? sessionTimeout)
        {

            _dbContextSecurity = new DCBankAdminContext();

            var tableName = "[Security].[UserInformation]";
            var setFieldName = "CurrentSessionExpiryDate";
            var setValue = sessionTimeout;
            var fieldName = "Id";
            var isGeneratePassword = _dbContextSecurity.Database.ExecuteSqlCommand("EXEC [Common].[USP_UpdateTableByAField] '" + tableName + "','" +
                setFieldName + "','" + setValue + "','" + fieldName + "','" + userId + "'");

        }

        public bool UpdateStatus(long userId, string status)
        {
            DCBankAdminContext _dbContextseNpSecurity = new DCBankAdminContext();
            return _dbContextseNpSecurity.Database.ExecuteSqlCommand("EXEC [Security].[USP_ChangeStatusByUserId] '" + userId + "','" + status + "'") > 0;
        }



      

        public UserInformation GetUserForForgotPassword(long userId, string username, string userVerificationCode)
        {
            return _dbContextSecurity.UserInformations.AsNoTracking().FirstOrDefault(u => u.Id == userId && u.UserName == username);
        }

     

        public UserInformation SelfRegistrationNewCustomer(UserInformation userInfo)
        {
            return this.InsertUser(userInfo);
        }
      

        public void UpdateUserPassword(UserInformation userInformation)
        {
            throw new NotImplementedException();
        }



        public UserInformation GetUserByUserId(long userId)
        {
            return _dbContextSecurity.UserInformations.Where(x => x.Id == userId).FirstOrDefault();
        }

        public UserInformation GetUserByUserName(string userName)
        {
            return _dbContextSecurity.UserInformations.Where(x => x.Email == userName).FirstOrDefault();
        }

      

        public UserInformation GetUserByEmail(string email)
        {
            return _dbContextSecurity.UserInformations.Where(x => x.Email == email).FirstOrDefault();
        }














    }
}
