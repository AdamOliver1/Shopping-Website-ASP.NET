using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IUserService
    {
        UserModel GetUserByUserName(string userNmae);
        void AddUser(UserModel user);
        void UpdateUser(UserModel user);
        bool IsValidUser(string userName);
        bool IsUserExcist(string userName,string password);       



    }
}
