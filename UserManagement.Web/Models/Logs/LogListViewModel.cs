using System.ComponentModel.DataAnnotations;
using System;
using UserManagement.Models;

namespace UserManagement.Web.Models.Logs;

public class LogListViewModel
{
    public List<LogListItemViewModel> Items { get; set; } = new();
}

public class LogListItemViewModel
{
    [Required]
    public long Id { get; set; }
    [Required]
    public long UserId { get; set; }
    [Required]
    public DateTime Time { get; set; }
    [Required]
    public LogActionType ActionTaken { get; set; }

    public string? Forename { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? DateOfBirth { get; set; }
}
