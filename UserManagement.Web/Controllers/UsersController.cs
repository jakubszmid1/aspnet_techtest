using System;
using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Logs;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly ILogService _logService;
    public UsersController(IUserService userService, ILogService logService)
    {
        _userService = userService;
        _logService = logService;
    }

    [HttpGet]
    public ViewResult List(bool? isActive = null)
    {
        IEnumerable<User> query;

        //if filtering by active state, use FilterByActive, otherwise display all users
        query = isActive.HasValue ? _userService.FilterByActive(isActive.Value) : _userService.GetAll();

        var items = query.Select(p => MapToUserListItemViewModel(p));

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    [HttpGet("add")]
    public ViewResult AddUser() => View();

    [HttpGet("delete/{id}")]
    public ViewResult DeleteUser(long id)
    {
        var user = _userService.GetById(id);
        if (user == null)
            return View("UserNotFound");

        var item = MapToUserListItemViewModel(user);

        return View(item);
    }

    [HttpGet("view/{id}")]
    public ViewResult ViewUser(long id)
    {
        //find the user to inspect by key
        var user = _userService.GetById(id);
        if (user == null)
            return View("UserNotFound");

        var item = MapToUserListItemViewModel(user);

        ViewBag.Logs = _logService.GetByUserId(id)
            .Select(p => MapToLogListItemViewModel(p))
            .OrderBy(p => p.Time)
            .Reverse();

        return View(item);
    }

    [HttpGet("edit/{id}")]
    public ViewResult EditUser(long id)
    {
        //find the user to inspect by key
        var user = _userService.GetById(id);
        if (user == null)
            return View("UserNotFound");

        var item = MapToUserListItemViewModel(user);

        return View(item);
    }

    [HttpPost("add")]
    public IActionResult AddUser(UserListItemViewModel userViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        var user = MapToUser(userViewModel);
        _userService.Add(user);
        _logService.Add(user, LogActionType.Create);
        return RedirectToAction("List");
    }

    [HttpPost("edit/{id}")]
    public IActionResult EditUser(UserListItemViewModel userViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(userViewModel);
            
        }
        var user = MapToUser(userViewModel);
        _logService.Add(user, LogActionType.Update);
        _userService.Update(user);
        return RedirectToAction("List");
    }

    [HttpPost("delete/{id}")]
    public IActionResult DeleteUserAction(long id)
    {
        if (!ModelState.IsValid)
        {
            return View();

        }
        var user = _userService.GetById(id);
        if (user == null)
            return View("UserNotFound");
        _logService.Add(user, LogActionType.Delete);
        _userService.Delete(id);
        return RedirectToAction("List");
    }

    private UserListItemViewModel MapToUserListItemViewModel(User user) => new()
    {
        Id = user.Id,
        Forename = user.Forename,
        Surname = user.Surname,
        Email = user.Email,
        IsActive = user.IsActive,
        DateOfBirth = user.DateOfBirth,
    };

    private User MapToUser(UserListItemViewModel user) => new()
    {
        Id = user.Id,
        Forename = user.Forename ?? "",
        Surname = user.Surname ?? "",
        Email = user.Email ?? "",
        IsActive = user.IsActive,
        DateOfBirth = user.DateOfBirth ?? DateTime.MinValue,
    };

    public LogListItemViewModel MapToLogListItemViewModel(Log log)
    {
        var user = _userService.GetById(log.UserId);
        return new LogListItemViewModel
        {
            Id = log.Id,
            UserId = log.UserId,
            Time = log.Time,
            ActionTaken = log.ActionTaken,
            Forename = log.Forename,
            Surname = log.Surname,
        };
    }
}
