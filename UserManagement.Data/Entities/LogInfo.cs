using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static UserManagement.Models.User;

namespace UserManagement.Models;

/// <summary>
/// This class represents deeper log informaton about a log
/// </summary>
///

public class LogInfo
{
    public long Id { get; set; }
    public string Message { get; set; } = default!;
}

public class UserLogInfo : LogInfo
{
    public string? Forename { get; set; } = default!;
    public string? Surname { get; set; } = default!;
    public string? Email { get; set; } = default!;
    public bool? IsActive { get; set; }
    public DateTime? DateOfBirth { get; set; } = default!;
}
