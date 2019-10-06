using System;
using DAL.Interface.Interfaces;
using BLL.Interface.Entities;
using System.Collections.Generic;
using System.IO;
using DAL.Contexts;

namespace DAL.Repositories 
{
    public class AccountRepository : IRepository
    {
        private const string Path = "file.bin";
        private static AccountRepository instance;
        private readonly AccountContext context = new AccountContext();

        /// <summary>
        /// Reads all accounts from file
        /// </summary>
        public IEnumerable<Account> ReadAccountsFromFile()
        {
            return context.Accounts;
        }

        public static AccountRepository GetInstance()
        {
            if (instance == null)
                instance = new AccountRepository();

            return instance;
        }

        private AccountRepository() { }

        /// <summary>
        /// Appends given account to repository
        /// </summary>
        /// <param name="account">Account to append</param>
        public void AppendAccountToFile(Account account)
        {
            context.Add(account);
            context.SaveChanges();
        }

        /// <summary>
        /// Overwrites current file with new set of accounts
        /// </summary>
        public void OverWriteFile(IEnumerable<Account> accounts)
        {
            foreach(var account in context.Accounts)
                context.Remove(account);

            context.AddRange(accounts);
        }
    }
}
