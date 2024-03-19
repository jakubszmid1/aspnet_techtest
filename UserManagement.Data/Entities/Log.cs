using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models;

/// <summary>
/// This class represents a log for any type of change to the database, for any entity
/// </summary>
///

public class Log
{
    //set the primary key and auto increment
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public long? previousLogId { get; set; } // can be null, can be used to link logs together and compare a history of changes
    public long UserId { get; set; }
    public DateTime Time { get; set; } = DateTime.Now;
    public LogActionType ActionTaken { get; set; }
    public string? Forename { get; set; } = default!;
    public string? Surname { get; set; } = default!;
    public string? Email { get; set; } = default!;
    public bool? IsActive { get; set; }
    public DateTime? DateOfBirth { get; set; } = default!;
}
