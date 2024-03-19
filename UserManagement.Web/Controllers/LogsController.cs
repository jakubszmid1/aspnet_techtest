using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Models;
using System;
using UserManagement.Web.Models.Logs;

namespace UserManagement.WebMS.Controllers;

[Route("logs")]
public class LogsController : Controller
{
    private readonly IUserService _userService;
    private readonly ILogService _logService;
    public LogsController(IUserService userService, ILogService logService)
    {
        _userService = userService;
        _logService = logService;
    }

    [HttpGet]
    public ViewResult List()
    {
        var items = _logService.GetAll()
            .Select(p => MapToLogListItemViewModel(p))
            .OrderBy(p => p.Time)
            .Reverse();

        var model = new LogListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    [HttpGet("view/{id}")]
    public ViewResult ViewLog(long id)
    {
        var log = _logService.GetByLogId(id).FirstOrDefault();
        if (log == null)
            return View("LogNotFound");

        var item = MapToLogListItemViewModel(log);

        ViewBag.ForenameChanged = _logService.ForenameChanged(log);
        ViewBag.SurnameChanged = _logService.SurnameChanged(log);
        ViewBag.EmailChanged = _logService.EmailChanged(log);
        ViewBag.IsActiveChanged = _logService.IsActiveChanged(log);
        ViewBag.DateOfBirthChanged = _logService.DateOfBirthChanged(log);

        return View(item);
    }

    public LogListItemViewModel MapToLogListItemViewModel(Log log)
    {
        var user = _userService.GetById(log.UserId);
        return new LogListItemViewModel
        {
            Id = log.Id,
            UserId = log.UserId,
            Time = log.Time,
            ActionTaken = log.ActionTaken,
            Forename = log?.Forename,
            Surname = log?.Surname,
            Email = log?.Email,
            IsActive = log?.IsActive,
            DateOfBirth = log?.DateOfBirth
        };
    }
}
