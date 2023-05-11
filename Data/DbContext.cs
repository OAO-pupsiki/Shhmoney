using Microsoft.EntityFrameworkCore;
using Shhmoney.Models;
using Shhmoney.Utils;
using System.Configuration;

namespace Shhmoney.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext() 
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            Database.OpenConnection();

            /*var adminRole = new Role
            {
                Name = "Admin",
                Description = "Simple admin role"
            };
            Roles.Add(adminRole);
            SaveChanges();
            var userRole = new Role
            {
                Name = "User",
                Description = "Simple user role"
            };
            Roles.Add(userRole);
            SaveChanges();
            string password = "1111";
            string hashedPassword = PasswordHasher.HashPassword(password);
            var admin = new User
            {
                Username = "admin",
                Password = hashedPassword,
                Email = "forinovegor@gmail.com",
                Role = adminRole
            };
            Users.Add(admin);
            SaveChanges();
            var user = new User
            {
                Username = "egor",
                Password = hashedPassword,
                Email = "forinovegor2023@gmail.com",
                Role = userRole
            };
            Users.Add(user);
            SaveChanges();
            var currency = new Currency
            {
                Code = "BYN",
                Value = 1
            };
            Currencies.Add(currency);
            SaveChanges();
            var account = new Account
            {
                Name = "Personal account",
                Balance = 0,
                PaymentType = 0,
                User = admin,
                Currency = currency
            };
            Accounts.Add(account);
            SaveChanges();
            var expenseCategory = new ExpenseCategory
            {
                Name = "Family",
                Description = "Category, that includes all family expenses",
                User = admin
            };
            ExpenseCategories.Add(expenseCategory);
            SaveChanges();
            var incomeCategory = new IncomeCategory
            {
                Name = "Business",
                Description = "Category, that includes all business incomes",
                User = admin
            };
            var incomeCategory1 = new IncomeCategory
            {
                Name = "Scholarship",
                Description = "Category, that response for scholarship",
                User = admin
            };
            IncomeCategories.Add(incomeCategory);
            IncomeCategories.Add(incomeCategory1);
            SaveChanges();
            var income = new Income
            {
                Name = "Scholarship payment",
                Description = "Some short description",
                Value = 180,
                DateTime = DateTime.UtcNow,
                User = admin,
                Account = account,
                IncomeCategory = incomeCategory1
            };
            var income1 = new Income
            {
                Name = "Business profit",
                Description = "Some short description",
                Value = 80,
                DateTime = DateTime.UtcNow,
                User = admin,
                Account = account,
                IncomeCategory = incomeCategory
            };
            Incomes.Add(income);
            Incomes.Add(income1);
            SaveChanges();
            var expense = new Expense
            {
                Name = "Shavuha",
                Description = "Some short description",
                Value = 8,
                DateTime = DateTime.UtcNow,
                User = admin,
                Account = account,
                ExpenseCategory = expenseCategory
            };
            Expenses.Add(expense);
            SaveChanges();*/
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            optionsBuilder.UseNpgsql(connectionString);
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<IncomeCategory> IncomeCategories { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<UserSession> Sessions { get; set; }
        public DbSet<Currency> Currencies { get; set; }
    }
}
