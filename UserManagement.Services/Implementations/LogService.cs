using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

internal class LogService : ILogService
{
    private readonly IDataContext _dataAccess;
    public LogService(IDataContext dataAccess) => _dataAccess = dataAccess;

    public IEnumerable<Log> GetAll() => _dataAccess.GetAll<Log>();
    public void Add(Log log)
    {
        //previous log id is the most recent log id for the same user
        log.previousLogId = _dataAccess.GetAll<Log>()
            .Where(x => x.UserId == log.UserId)
            .OrderByDescending(x => x.Time)
            .Select(x => x.Id)
            .FirstOrDefault();
        _dataAccess.Create(log);
    }
    public void Add(User user, LogActionType action)
    {
        var log = new Log
        {
            UserId = user.Id,
            ActionTaken = action
        };

        log.Forename = user.Forename;
        log.Surname = user.Surname;
        log.Email = user.Email;
        log.IsActive = user.IsActive;
        log.DateOfBirth = user.DateOfBirth;
        Add(log);
    }

    //get by id, there will only be one log per log id
    public IEnumerable<Log> GetByLogId(long id) => (
               _dataAccess.GetAll<Log>()
               .Where(x => x.Id == id)
        );

    public IEnumerable<Log> GetByUserId(long userId) => (
                _dataAccess.GetAll<Log>()
               .Where(x => x.UserId == userId)
        );

    public IEnumerable<Log> GetByDate(DateTime date) => (
                _dataAccess.GetAll<Log>()
                .Where(x => x.Time.Date == date.Date)
        );

    /// <summary>
    /// Get a subset of logs by page number, with a specified number of logs per page
    /// </summary>
    /// <param name="logsPerPage">
    /// The number of logs to return per page
    /// </param>
    /// <param name="page">
    /// The page number to return, starting from 1
    /// </param>
    /// <returns></returns>
    public IEnumerable<Log> GetPage(int logsPerPage, int page) => (
                _dataAccess.GetAll<Log>()
                .OrderByDescending(x => x.Time)
                .Skip((page - 1) * logsPerPage)
                .Take(logsPerPage)
        );

    public bool ForenameChanged(Log log)
    {
        //checks if the previous log id exists, if it does, attempt to grab the previous log
        var previousLog = log.previousLogId.HasValue
        ? GetByLogId((long)log.previousLogId)?.FirstOrDefault()
        : null;

        //if the previous log is null, it's the first log, so the forename has changed
        return previousLog == null || log.Forename != previousLog.Forename;
    }

    public bool SurnameChanged(Log log)
    {
        var previousLog = log.previousLogId.HasValue
        ? GetByLogId((long)log.previousLogId)?.FirstOrDefault()
        : null;

        return previousLog == null || log.Surname != previousLog.Surname;
    }

    public bool EmailChanged(Log log)
    {
        var previousLog = log.previousLogId.HasValue
        ? GetByLogId((long)log.previousLogId)?.FirstOrDefault()
        : null;

        return previousLog == null || log.Email != previousLog.Email;
    }

    public bool IsActiveChanged(Log log)
    {
        var previousLog = log.previousLogId.HasValue
        ? GetByLogId((long)log.previousLogId)?.FirstOrDefault()
        : null;

        return previousLog == null || log.IsActive != previousLog.IsActive;
    }

    public bool DateOfBirthChanged(Log log)
    {
        var previousLog = log.previousLogId.HasValue
        ? GetByLogId((long)log.previousLogId)?.FirstOrDefault()
        : null;

        return previousLog == null || log.DateOfBirth != previousLog.DateOfBirth;
    }
}
