using System;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using DependencyResolver;
using Ninject;

namespace ConsolePL
{
    class Program
    {
        private static readonly IKernel resolver;

        static Program()
        {
            resolver = new StandardKernel();
            resolver.ConfigurateResolver();
        }

        static void Main(string[] args)
        {
            IAccountService service = resolver.Get<IAccountService>();
            
            service.CreateAccount(new Account("Michlo1", "Mikh1", 100, 5, AccountType.Base));
            service.CreateAccount(new Account("Michlo2", "Mikh1", 200, 15, AccountType.Gold));
            service.CreateAccount(new Account("Michlo3", "Mikh1", 300, 25, AccountType.Premium));

            service.PrintAllAccounts();
        }
    }
}
