using BLL.Interface.Entities;
using DAL.Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class Mapper
    {
        public static Account ConvertToAccount(this DalAccount dalAccount)
        {
            return (Account)Activator.CreateInstance(
                typeof(AccountType),
                dalAccount.Id,
                dalAccount.OwnerFirstName,
                dalAccount.OwnerLastName,
                dalAccount.Amount,
                dalAccount.Points);
        }
        public static DalAccount ConvertToDalAccount(this Account account)
        {
            return new DalAccount
            {
                Type = account.Type,
                Points = account.Points,
                Amount = account.Amount,
                Id = account.Id,
                OwnerFirstName = account.OwnerFirstName,
                OwnerLastName = account.OwnerLastName,
            };
        }
    }
}