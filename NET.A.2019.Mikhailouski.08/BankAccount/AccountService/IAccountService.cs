using System.Collections.Generic;
using AccountModel;

namespace AccountServiceN
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAllAccounts();
        void AddAmount(int id, decimal amount);
        void DivAmount(int id, decimal amount);
        void CloseAccount(int id);
        void CreateAccount(Account account);
        void PrintAllAccounts();
    }
}
