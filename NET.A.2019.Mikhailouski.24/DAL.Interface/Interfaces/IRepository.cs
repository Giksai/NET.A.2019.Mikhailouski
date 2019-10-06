using System;
using BLL.Interface.Entities;
using System.Collections.Generic;

namespace DAL.Interface.Interfaces
{
    public interface IRepository
    {
        IEnumerable<Account> ReadAccountsFromFile();
        void AppendAccountToFile(Account account);
        void OverWriteFile(IEnumerable<Account> accounts);
    }
}
