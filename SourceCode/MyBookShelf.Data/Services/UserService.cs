using System.Linq;
using MyBookShelf.Business.Contexts;
using MyBookShelf.Business.Entities;

namespace MyBookShelf.Data.Services;

public class UserService
{
    public DatabaseContext _context;
    
    public UserService()
    {
        _context = new DatabaseContext();
    }

    public bool UserExist(string userName, string password)
    {
        var userExist = false;
        var userList = from m in _context.Users select m;
        userList = userList.Where(s => (s.UserName.Contains(userName) || s.EMailAddress.Contains(userName)) && s.Password.Equals(password) && !s.IsDeleted);
        userExist = userList.Any();
        return userExist;
    }
    
    public Result UserLogin(string userName, string password)
    {
        var result = new Result();
        result.IsError = true;
        result.ActiveUser = null;
        var userList = from m in _context.Users select m;
        userList = userList.Where(s => (s.UserName.Equals(userName) || s.EMailAddress.Equals(userName)) && !s.IsDeleted);
        if (!userList.Any())
            result.Id = "There is no user with this user name";
        else
        {
            userList = userList.Where(s => (s.Password.Equals(password)));    
            if (!userList.Any())
                result.Id = "Password is wrong";
            else
            {
                result.Id = userList.ToList()[0].Id;
                result.ActiveUser = userList.ToList()[0];
                result.IsError = false;
            }
        }
        return result;
    }

    public class Result
    {
        public string Id { get; set; }
        public bool IsError { get; set; }
        public User ActiveUser { get; set; }
    }
}