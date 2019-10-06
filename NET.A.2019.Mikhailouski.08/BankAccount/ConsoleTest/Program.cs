using System;
using AccountModel;
using AccountServiceN;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IAccountService AccService = new AccountService();

            //AccService.CreateAccount(new Account(0, "Michlo", "Mikhailo", 100, 5, AccountType.Base));
            //AccService.CreateAccount(new Account(1, "Michlo1", "Mikhailo1", 200, 10, AccountType.Gold));
            //AccService.CreateAccount(new Account(2, "Michlo2", "Mikhailo2", 300, 15, AccountType.Premium));

            //AccService.CloseAccount(2);

            //AccService.DivAmount(2, 100);

            //AccService.AddAmount(0, 10);

            AccService.PrintAllAccounts();

        }
    }
}
