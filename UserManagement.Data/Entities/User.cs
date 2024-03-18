using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models;

public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [StringLength(30, MinimumLength = 1, ErrorMessage = "Must be 1-30 characters long")]
    [Required(ErrorMessage = "Forename is Required")]
    public string Forename { get; set; } = default!;

    [StringLength(30, MinimumLength = 1, ErrorMessage = "Must be 1-30 characters long")]
    [Required(ErrorMessage = "Surname is Required")]
    public string Surname { get; set; } = default!;

    [Required(ErrorMessage = "Email is Required")]
    //email regex requiring @ and . for domain
    [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "User must be either active, or inactive")]
    public bool IsActive { get; set; }

    [Required(ErrorMessage = "Date of Birth is Required")]
    [DataType(DataType.Date)]
    [InPast(ErrorMessage = "Date of Birth must be in the past")]
    public DateTime DateOfBirth { get; set; } = default!;

    //custom validation attribute to check if date of birth is in the past
    public class InPastAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime date)
            {
                return date < DateTime.Now;
            }
            return false;
        }
    }   
}
