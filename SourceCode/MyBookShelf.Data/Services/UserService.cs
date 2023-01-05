using System.Linq;
using MyBookShelf.Business.Contexts;

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
    
    public string UserLogin(string userName, string password)
    {
        var result = string.Empty;
        var userExist = false;
        var userList = from m in _context.Users select m;
        userList = userList.Where(s => (s.UserName.Equals(userName) || s.EMailAddress.Equals(userName)) && !s.IsDeleted);
        if (!userList.Any())
            result = "There is no user with this user name";
        else
        {
            userList = userList.Where(s => (s.Password.Equals(password)));    
            if (!userList.Any())
                result = "Password is wrong";
        }
        return result;
    }
}