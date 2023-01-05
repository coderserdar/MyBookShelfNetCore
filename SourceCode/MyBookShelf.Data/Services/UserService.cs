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
}