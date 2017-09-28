namespace BankSystem.Core
{
    using BankSystem.Data;
    using BankSystem.Models;
    using BankSystem.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public class Engine
    {
        private BankSystemController _bankSystemController;

        public Engine(BankSystemController bankSystemController)
        {
            this._bankSystemController = bankSystemController;
        }

        public void Run()
        {
            using (BankSystemContext context = new BankSystemContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                string input;
                while ((input = Console.ReadLine()) != "Exit")
                {
                    string[] inputArgs = input
                        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string command = inputArgs[0];
                    string[] args = inputArgs.Skip(1).ToArray();

                    Type type = typeof(Engine);
                    ConstructorInfo ctor = type.GetConstructor(new Type[] { typeof(BankSystemController) });
                    Engine engine = ctor.Invoke(new object[] { this._bankSystemController }) as Engine;
                    MethodInfo method = type.GetMethod(command, BindingFlags.Instance | BindingFlags.NonPublic);
                    if (method == null)
                    {
                        Console.WriteLine("Invalid command!");
                        continue;
                    }
                    method.Invoke(engine, new object[] { context, args });
                }
            }
        }

        private void Register(BankSystemContext context, string[] args)
        {
            string username = args[0];
            string password = args[1];
            string email = args[2];

            if (context.Users.Any(u => u.Username == username))
            {
                Console.WriteLine("Username is taken!");
                return;
            }

            Regex regex = new Regex("^[^\\d]([A-Za-z0-9]+){3}$");

            if (!regex.IsMatch(username))
            {
                Console.WriteLine("Incorrect username!");
                return;
            }

            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit))
            {
                Console.WriteLine("Incorrect password!");
                return;
            }

            regex = new Regex("^[^_.-].+[^_.-]@\\w+.\\w+$");

            if (!regex.IsMatch(email))
            {
                Console.WriteLine("Incorrect email!");
                return;
            }

            User user = new User
            {
                Username = username,
                Password = password,
                EmailAddress = email
            };

            context.Users.Add(user);

            context.SaveChanges();

            Console.WriteLine($"{user.Username} was registered in the system.");
        }

        private void Login(BankSystemContext context, string[] args)
        {
            string username = args[0];
            string password = args[1];

            User user = context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null && user.Password == password)
            {
                this._bankSystemController.LogedUser = user;
                Console.WriteLine($"Succesfully logged in {user.Username}");
                return;
            }

            Console.WriteLine("Invalid username / password");
        }

        private void Logout(BankSystemContext context, string[] args)
        {
            if (this._bankSystemController.LogedUser == null)
            {
                Console.WriteLine("Cannot log out. No user was logged in.");
                return;
            }

            string logedUsername = this._bankSystemController.LogedUser.Username;
            this._bankSystemController.LogedUser = default(User);
            Console.WriteLine($"User {logedUsername} successfully logged out");
        }

        private void Add(BankSystemContext context, string[] args)
        {
            if (this._bankSystemController.LogedUser == null)
            {
                Console.WriteLine($"You should Login first!");
                return;
            }

            string acountType = args[0];

            decimal initialBallance = decimal.Parse(args[1]);

            Account account = default(Account);

            switch (acountType)
            {
                case "SavingsAccount":
                    double rate = double.Parse(args[2]);
                    account = new SavingAccount
                    {
                        Ballance = initialBallance,
                        AccountNumber = RandomCombinationGenerator.Generate(),
                        Rate = rate
                    };
                    break;

                case "CheckingAccount":
                    double fee = double.Parse(args[2]);
                    account = new CheckingAccount
                    {
                        Ballance = initialBallance,
                        AccountNumber = RandomCombinationGenerator.Generate(),
                        Fee = fee
                    };
                    break;

                default:
                    Console.WriteLine("Invalid command!");
                    return;
            }

            this._bankSystemController.LogedUser.Accounts.Add(account);
            context.Users.Find(this._bankSystemController.LogedUser.Id).Accounts.Add(account);

            context.SaveChanges();

            Console.WriteLine($"Succesfully added account with number {account.AccountNumber}");
        }

        private void ListAccounts(BankSystemContext context, string[] args)
        {
            if (this._bankSystemController.LogedUser == null)
            {
                Console.WriteLine("You should Login first!");
                return;
            }
            int userId = this._bankSystemController.LogedUser.Id;

            List<CheckingAccount> checkingAccounts = context
                .CheckingAccounts
                .Where(ca => ca.OwnerId == userId)
                .ToList();

            List<SavingAccount> savingAccounts = context
                .SavingAccounts
                .Where(sa => sa.OwnerId == userId)
                .ToList();

            Console.WriteLine($"Accounts for user {this._bankSystemController.LogedUser}");
            Console.WriteLine("Saving Accounts:");
            Console.WriteLine($"{string.Join("\r\n", savingAccounts)}");
            Console.WriteLine("Cheking Accounts:");
            Console.WriteLine($"{string.Join("\r\n", checkingAccounts)}");
        }

        private void Deposit(BankSystemContext context, string[] args)
        {
            if (this._bankSystemController.LogedUser == null)
            {
                Console.WriteLine("You should Login first!");
                return;
            }

            string accountNumber = args[0];
            decimal money = decimal.Parse(args[1]);
            Account account =
                context
                    .Accounts
                    .FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (account == null)
            {
                Console.WriteLine("Invalid account number!");
                return;
            }

            context.SaveChanges();

            Console.WriteLine($"Account {account.AccountNumber} has balance of {account.Ballance}");
        }

        private void Withdraw(BankSystemContext context, string[] args)
        {
            if (this._bankSystemController.LogedUser == null)
            {
                Console.WriteLine("You should Login first!");
                return;
            }

            string accountNumber = args[0];
            decimal money = decimal.Parse(args[1]);

            Account account = context
                .Accounts
                .FirstOrDefault(a => a.AccountNumber == accountNumber);

            account.WithdrawMoney(money);
            context.SaveChanges();

            Console.WriteLine($"Account {account.AccountNumber} has balance of {account.Ballance}");
        }

        private void DeductFee(BankSystemContext context, string[] args)
        {
            if (this._bankSystemController.LogedUser == null)
            {
                Console.WriteLine("You should Login first!");
                return;
            }

            string accountNumber = args[0];

            if (!(context
                .Accounts
                .FirstOrDefault(a => a.AccountNumber == accountNumber) is CheckingAccount account))
            {
                Console.WriteLine("Invalid account number!");
                return;
            }

            account.DeductFee();
            context.SaveChanges();

            Console.WriteLine($"Deducted fee of {account.AccountNumber}. Current balance: {account.Ballance:F2}");
        }

        private void AddInterest(BankSystemContext context, string[] args)
        {
            if (this._bankSystemController.LogedUser == null)
            {
                Console.WriteLine("You should Login first!");
                return;
            }

            string accountNumber = args[0];
            SavingAccount account = context
                .SavingAccounts
                .FirstOrDefault(sa => sa.AccountNumber == accountNumber);

            if (account == null)
            {
                Console.WriteLine("Invalid account number!");
                return;
            }

            account.AddRate();
            context.SaveChanges();

            Console.WriteLine($"Added interest to {account.AccountNumber}. Current balance: {account.Ballance:F2}");
        }

        private void Exit(BankSystemContext context, string[] args)
        {
            Console.WriteLine("Bye!");
            Environment.Exit(0);
        }
    }
}