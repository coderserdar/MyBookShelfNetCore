using System.ComponentModel.DataAnnotations;

namespace MyBookShelf.Web.Models;

public class RegisterViewModel
{
    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "E-Mail Address:")]
    public string EMailAddress { get; init; }
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password:")]
    public string Password { get; init; }
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password (Again):")]
    public string PasswordAgain { get; init; }
    public string? OperationMessage { get; set; }
}