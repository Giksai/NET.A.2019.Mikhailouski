using System.Collections.Generic;
using AccountModel;

namespace AccountStorageN
{
    public interface IAccountStorage
    {
        IEnumerable<Account> ReadAccountsFromFile();
        void AppendAccountToFile(Account account);
        void OverWriteFile(IEnumerable<Account> accounts);
    }
}
