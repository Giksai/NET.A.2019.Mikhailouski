using System;
using BLL.Interface.Interfaces;
using BLL.Interface.Entities;
using System.Collections.Generic;
using DAL.Interface.Interfaces;
using DAL.Repositories;
using System.Linq;

namespace BLL.ServiceImplementation
{
    public class AccountService : IAccountService
    {
        private readonly IRepository _accountStorage;
        private List<Account> accountsInMemory;

        public AccountService()
        {
            _accountStorage = AccountRepository.GetInstance();
            accountsInMemory = _accountStorage.ReadAccountsFromFile().ToList();
        }

        /// <summary>
        /// Returns all accounts 
        /// </summary>
        public IEnumerable<Account> GetAllAccounts()
        {
            return accountsInMemory;
        }

        public void PrintAllAccounts()
        {
            foreach (var acc in GetAllAccounts())
            {
                Console.WriteLine(acc);
            }
        }

        /// <summary>
        /// Increases account's amount
        /// </summary>
        public void AddAmount(int id, decimal amount)
        {
            if (id < 0) throw new ArgumentException();

            var account = FindAccount(id);
            if (account == null)
            {
                Console.WriteLine("No such account found!");
                return;
            }

            if (account.Status == AccountStatus.Closed) throw new ArgumentException("Account is closed");

            account.Amount += amount;

            //Strategy pattern
            IPointsLogic IPL = new PointsLogic();
            IPL.EvaluatePoints(account);

            SaveChanges();
        }


        /// <summary>
        /// Decreases account's amount
        /// </summary>
        public void DivAmount(int id, decimal amount)
        {
            if (id < 0) throw new ArgumentException();

            var account = FindAccount(id);
            if (account == null)
            {
                Console.WriteLine("No such account found!");
                return;
            }

            if (account.Status == AccountStatus.Closed) throw new ArgumentException("Account is closed");
            account.Amount = account.Amount - amount;

            SaveChanges();
        }

        /// <summary>
        /// Creates new account
        /// </summary>
        public void CreateAccount(Account account)
        {
            if (account == null) throw new ArgumentNullException();

            accountsInMemory.Add(account);
            _accountStorage.AppendAccountToFile(account);
        }

        /// <summary>
        /// Assigns account status to closed
        /// </summary>
        public void CloseAccount(int id)
        {
            if (id < 0) throw new ArgumentException();

            var account = FindAccount(id);
            if (account == null)
            {
                Console.WriteLine("No such account found!");
                return;
            }
            account.Status = AccountStatus.Closed;

            SaveChanges();
        }

        private void SaveChanges()
        {
            _accountStorage.OverWriteFile(accountsInMemory);
        }

        private Account FindAccount(int id)
        {
            return accountsInMemory.FirstOrDefault(acc => acc.Id == id);

            //var accounts = _accountStorage.ReadAccountsFromFile().ToList();
            //return accounts.FirstOrDefault(account => account.Id == id);
        }
    }

    public class PointsLogic : IPointsLogic
    {
        public virtual void EvaluatePoints(Account account)
        {
            account.Points += (int)account.Type;
        }
    }

    public interface IPointsLogic
    {
        void EvaluatePoints(Account account);
    }
}
