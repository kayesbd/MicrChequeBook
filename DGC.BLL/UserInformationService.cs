using System;
using System.Collections.Generic;
using AutoMapper;
using KBZ.Administration.Domain.Model;
using System.Web;
using KBZ.BLL.Security;
using KBZ.DAL.BankAdmin.Repository;
using KBZ.DAL.BankAdmin;
using DGC.BLL.App_Start;

namespace  KBZ.BLL.Security
{
    public partial interface IUserInformationService 
    {
        List<UserInformationModel> GetUserList();
        bool IsUsernameExist(string username);
        KeyValuePair<string, bool> ValidateUserInformation(UserInformationModel userInformation, bool checkPersonalInfo);
        UserInformationModel GetUser(long userId);
        UserInformationModel GetUser(string username);
        UserInformationModel GetUserByEmail(string email);
        UserInformationModel CreateUser(UserInformationModel userInformation);
        void UpdateUser(UserInformationModel userInformation);
        void UpdatLoginUserInformationAsync(UserInformationModel userInformation);
        bool UpdatePasswordForForgotPassword(UserInformationModel userInformation);
        void DeleteUserInformation(long userId);
        void UpdateSessionForUser(long userId, DateTime? sessionTimeout);
        bool UpdateStatus(long userId, string status);
        KeyValuePair<bool, string> ValidateUserNewPassword(UserInformationModel userInformation, string newPassword);
    }

    public class UserInformationService : IUserInformationService
    {
       
        private readonly UserInformationModel _loginUserInformation;
        private readonly IUserInformationRepository _userInformationRepository;
        public AutoMapper.Mapper map;
        public UserInformationService(IUserInformationRepository userInformationRepository)
            
        {
            _userInformationRepository = userInformationRepository;
           

            var currentContext = HttpContext.Current;
            if (currentContext != null)
            {
                if (HttpContext.Current.Session["UserInformation"] != null)
                {
                    _loginUserInformation = (UserInformationModel)HttpContext.Current.Session["UserInformation"];
                }
            }
        }

        public KeyValuePair<string, bool> ValidateUserInformation(UserInformationModel userInformation, bool checkPersonalInfo)
        {
            if (string.IsNullOrWhiteSpace(userInformation.UserName))
            {
                return new KeyValuePair<string, bool>("Username is required.", false);
            }
         

            if (!string.IsNullOrWhiteSpace(userInformation.UserName) && (userInformation.UserName.ToLower().Contains("admin") || userInformation.UserName.ToLower().Contains("administrator")))
            {
                return new KeyValuePair<string, bool>("Please remove 'admin' or 'administrator' word from the user name.", false);
            }

            return new KeyValuePair<string, bool>("", true);
        }


        public List<UserInformationModel> GetUserList()
        {
            var user = _userInformationRepository.GetUserList();
            return map.Map<List<UserInformation>, List<UserInformationModel>>(user);
        }


        public bool IsUsernameExist(string username)
        {
            return _userInformationRepository.IsUsernameExist(username);
        }

        public UserInformationModel GetUser(long userId)
        {
            if (userId != 0)
            {
                var user = _userInformationRepository.GetUserByUserId(userId);
                return map.Map<UserInformation, UserInformationModel>(user);
            }
            return null;
        }

        public UserInformationModel GetUser(string username)
        {
            var user = _userInformationRepository.GetUserByUserName(username);
            return map.Map<UserInformation, UserInformationModel>(user);
        }


        public UserInformationModel GetUserByEmail(string email)
        {
            var user = _userInformationRepository.GetUserByEmail(email);
            return map.Map<UserInformation, UserInformationModel>(user);
        }

        public UserInformationModel CreateUser(UserInformationModel userInformation)
        {
            userInformation.UserName = userInformation.UserName.ToLower();
           
            userInformation.Password = Authenticator.GetHashPassword(userInformation.Password);
            var user = map.Map<UserInformationModel, UserInformation>(userInformation);
            return map.Map<UserInformation, UserInformationModel>(_userInformationRepository.InsertUser(user));
        }

        public void UpdateUser(UserInformationModel userInformation)
        {
            
            var user = map.Map<UserInformationModel, UserInformation>(userInformation);
            _userInformationRepository.UpdateUser(user);
        }

        public void DeleteUserInformation(long userId)
        {
            _userInformationRepository.DeleteUser(userId);
        }

        public void UpdatLoginUserInformationAsync(UserInformationModel userInformation)
        {
            userInformation.AccessTokenValidityPeriod  = DateTime.Now.AddMinutes(5);
            var user = map.Map<UserInformationModel, UserInformation>(userInformation);
            _userInformationRepository.UpdateUserAsync(user);
        }



        public bool UpdatePasswordForForgotPassword(UserInformationModel userInformation)
        {
          
            return false;
        }

        public void UpdateSessionForUser(long userId, DateTime? sessionTimeout)
        {
            _userInformationRepository.UpdateSessionForUser(userId, sessionTimeout);
        }

        public bool UpdateStatus(long userId, string status)
        {
            return _userInformationRepository.UpdateStatus(userId, status);
        }

        public KeyValuePair<bool, string> ValidateUserNewPassword(UserInformationModel userInfo, string newPassword)
        {
            if (userInfo == null)
            {
                return new KeyValuePair<bool, string>(false, "User information is not found.");
            }
          
            if (string.IsNullOrWhiteSpace(newPassword))
            {
                return new KeyValuePair<bool, string>(false, "Please provide your new password.");
            }
           
            if (newPassword.Length < 6)
            {
                return new KeyValuePair<bool, string>(false, "Password minimum lenth is 6");
            }
            return new KeyValuePair<bool, string>(true, "");
        }
    }
}

