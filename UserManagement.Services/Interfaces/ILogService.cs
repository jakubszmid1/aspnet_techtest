using System.Collections.Generic;
using UserManagement.Models;
using System;

namespace UserManagement.Services.Domain.Interfaces;
public interface ILogService
{
    void Add(Log log);
    void Add(User user, LogActionType action);
    IEnumerable<Log> GetAll();
    IEnumerable<Log> GetByUserId(long userId);
    IEnumerable<Log> GetByLogId(long logId);
    IEnumerable<Log> GetByDate(DateTime date);
    IEnumerable<Log> GetPage(int logsPerPage, int page);
    bool ForenameChanged(Log log);
    bool SurnameChanged(Log log);
    bool EmailChanged(Log log);
    bool IsActiveChanged(Log log);
    bool DateOfBirthChanged(Log log);
}
