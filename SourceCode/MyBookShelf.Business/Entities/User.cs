using System.ComponentModel.DataAnnotations;

namespace MyBookShelf.Business.Entities;

public class User
{
    [Key]
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string EMailAddress { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsDeleted { get; set; }
}