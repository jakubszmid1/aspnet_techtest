using System;
using static UserManagement.Models.User;
using System.ComponentModel.DataAnnotations;
using UserManagement.Web.Models.Logs;

namespace UserManagement.Web.Models.Users;

public class UserListViewModel
{
    public List<UserListItemViewModel> Items { get; set; } = new();
}

public class UserListItemViewModel
{
    [Required]
    public long Id { get; set; }

    [StringLength(30, MinimumLength = 1, ErrorMessage = "Must be 1-30 characters long")]
    [Required(ErrorMessage = "Forename is Required")]
    public string? Forename { get; set; }

    [StringLength(30, MinimumLength = 1, ErrorMessage = "Must be 1-30 characters long")]
    [Required(ErrorMessage = "Surname is Required")]
    public string? Surname { get; set; }

    [Required(ErrorMessage = "Email is Required")]
    //email regex requiring @ and . for domain
    [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "User must be either active, or inactive")]
    public bool IsActive { get; set; }

    [Required(ErrorMessage = "Date of Birth is Required")]
    [DataType(DataType.Date, ErrorMessage = "Date of Birth is Required")] // case when no date is selected
    [InPast(ErrorMessage = "Date of Birth must be in the past")]
    public DateTime? DateOfBirth { get; set; }
}

public class UserListItemWithLogsViewModel : UserListItemViewModel
{
    public List<LogListItemViewModel> Logs { get; set; } = new();
}
