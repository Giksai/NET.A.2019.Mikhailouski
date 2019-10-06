using System;
using DAL.Interface.Interfaces;
using BLL.Interface.Entities;
using System.Collections.Generic;
using System.IO;

namespace DAL.Repositories 
{
    public class AccountRepository : IRepository
    {
        private const string Path = "file.bin";
        private static AccountRepository instance;

        /// <summary>
        /// Reads all accounts from file
        /// </summary>
        public IEnumerable<Account> ReadAccountsFromFile()
        {
            var accounts = new List<Account>();
            using (var br = new BinaryReader(File.Open(Path, FileMode.OpenOrCreate,
                FileAccess.Read, FileShare.Read)))
            {
                while (br.PeekChar() > -1)
                {
                    var account = Reader(br);
                    accounts.Add(account);
                }
            }

            return accounts;
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
            if (!File.Exists(Path)) throw new Exception("File does not exists!");

            using (var bw = new BinaryWriter(File.Open(Path, FileMode.Append,
                FileAccess.Write, FileShare.None)))
            {
                Writer(bw, account);
            }
        }

        /// <summary>
        /// Overwrites current file with new set of accounts
        /// </summary>
        public void OverWriteFile(IEnumerable<Account> accounts)
        {
            using (var bw = new BinaryWriter(File.Open(Path, FileMode.Create,
                FileAccess.Write, FileShare.None)))
            {
                foreach (var account in accounts)
                    Writer(bw, account);
            }
        }

        private static void Writer(BinaryWriter binary, Account account)
        {
            binary.Write(account.Id);
            binary.Write(account.OwnerFirstName);
            binary.Write(account.OwnerLastName);
            binary.Write(account.Amount);
            binary.Write(account.Points);
            binary.Write(account.Status.ToString());
            binary.Write(account.Type.ToString());

        }

        private static Account Reader(BinaryReader binary)
        {
            var id = binary.ReadInt32();
            var ownerFirstName = binary.ReadString();
            var ownerLastName = binary.ReadString();
            var amount = binary.ReadDecimal();
            var points = binary.ReadInt32();
            var status = binary.ReadString();
            var type = binary.ReadString();

            return new Account()
            {
                Id = id,
                OwnerFirstName = ownerFirstName,
                OwnerLastName = ownerLastName,
                Amount = amount,
                Points = points,
                Status = (AccountStatus)Enum.Parse(typeof(AccountStatus), status),
                Type = (AccountType)Enum.Parse(typeof(AccountType), type)
            };

        }
    }
}
