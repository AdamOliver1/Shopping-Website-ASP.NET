using Dal;
using Entities;
using Services.Interfaces;
using System;
using System.Linq;

namespace Services
{
    public class UserService : IUserService
    {
        MyContext _context;
        public UserService(MyContext contex)
        {
            _context = contex;
        }

        public void AddUser(UserModel user)
        {
            if (_context.Users.FirstOrDefault(u => u.UserName == user.UserName) != null) return;
            _context.Add(user);
            _context.SaveChanges();
        }

        public UserModel GetUserByUserName(string userNmae)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userNmae);
        }

        public void UpdateUser(UserModel user)
        {
            var userFromDb = _context.Users.FirstOrDefault(u => u.UserName == user.UserName); 
            if (userFromDb == null) throw new Exception();
            userFromDb.BirthDate = user.BirthDate;
            userFromDb.Password = user.Password;
            userFromDb.FirstName = user.FirstName;
            userFromDb.LastName = user.LastName;
            userFromDb.Email = user.Email;
         
            _context.Users.Update(userFromDb);
            _context.SaveChanges();
        }

        public bool IsValidUser(string userName)
        {          
            return GetUserByUserName(userName) == null;
        }

        public bool IsUserExcist(string userName, string password)
        {          
            return _context.Users.FirstOrDefault(u => u.Password == password && u.UserName == userName) != null;
        }
    }
}
