using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Entities;

namespace DAL.Interface.Interfaces
{
    public class DalAccount
    {
        public int Id { get; set; }
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public decimal Amount { get; set; }
        public int Points { get; set; }
        public AccountStatus Status { get; set; }
        public AccountType Type { get; set; }
    }
}