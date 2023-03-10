using System.ComponentModel.DataAnnotations;

namespace MyBookShelf.Web.Models;

public class LoginViewModel
{
    [Required]
    [Display(Name = "User Name:")]
    public string UserName { get; init; }
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password:")]
    public string Password { get; init; }
    public string? OperationMessage { get; set; }
}