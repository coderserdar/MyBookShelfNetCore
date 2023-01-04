using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MyBookShelf.Business.Entities;

public class User
{
    [Key]
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool IsDeleted { get; set; }
}