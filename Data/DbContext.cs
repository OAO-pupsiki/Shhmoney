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

            var role = Roles.FirstOrDefault(c => c.Name == "User");
            if (role == null)
            {
                role = new Role
                {
                    Name = "User",
                    Description = "Simple user role"
                };
                var dbItem = Roles.Add(role);
                this.SaveChanges();
            }

            var user = Users.FirstOrDefault(c => c.Username == "admin");
            if (user == null)
            {
                user = new User {Username = "admin", 
                    Password = "0ffe1abd1a08215353c233d6e009613e95eec4253832a761af28ff37ac5a150c",
                    Email = "admin@admin.com", 
                    RoleId = role.Id };
                var dbItem = Users.Add(user);
                this.SaveChanges();
            }

            var existingCategory = ExpenseCategories.FirstOrDefault(c => c.Name == "Еда");
            if (existingCategory == null)
            {
                var expenseCategory = new ExpenseCategory { Description = "", Name = "Еда", UserId = user.Id, IsBased = true };
                var dbItem = ExpenseCategories.Add(expenseCategory);
                this.SaveChanges();
            }

            existingCategory = ExpenseCategories.FirstOrDefault(c => c.Name == "Транспорт");
            if (existingCategory == null)
            {
                var expenseCategory = new ExpenseCategory { Description = "", Name = "Транспорт", UserId = user.Id, IsBased = true };
                var dbItem = ExpenseCategories.Add(expenseCategory);
                this.SaveChanges();
            }
            existingCategory = ExpenseCategories.FirstOrDefault(c => c.Name == "Спорт");
            if (existingCategory == null)
            {
                var expenseCategory = new ExpenseCategory { Description = "", Name = "Спорт", UserId = user.Id, IsBased = true };
                var dbItem = ExpenseCategories.Add(expenseCategory);
                this.SaveChanges();
            }

            existingCategory = ExpenseCategories.FirstOrDefault(c => c.Name == "Здоровье");
            if (existingCategory == null)
            {
                var expenseCategory = new ExpenseCategory { Description = "", Name = "Здоровье", UserId = user.Id, IsBased = true };
                var dbItem = ExpenseCategories.Add(expenseCategory);
                this.SaveChanges();
            }
            existingCategory = ExpenseCategories.FirstOrDefault(c => c.Name == "Питомцы");
            if (existingCategory == null)
            {
                var expenseCategory = new ExpenseCategory { Description = "", Name = "Питомцы", UserId = user.Id, IsBased = true };
                var dbItem = ExpenseCategories.Add(expenseCategory);
                this.SaveChanges();
            }

            existingCategory = ExpenseCategories.FirstOrDefault(c => c.Name == "Подарок");
            if (existingCategory == null)
            {
                var expenseCategory = new ExpenseCategory { Description = "", Name = "Подарок", UserId = user.Id, IsBased = true };
                var dbItem = ExpenseCategories.Add(expenseCategory);
                this.SaveChanges();
            }
            existingCategory = ExpenseCategories.FirstOrDefault(c => c.Name == "Связь");
            if (existingCategory == null)
            {
                var expenseCategory = new ExpenseCategory { Description = "", Name = "Связь", UserId = user.Id, IsBased = true };
                var dbItem = ExpenseCategories.Add(expenseCategory);
                this.SaveChanges();
            }

            existingCategory = ExpenseCategories.FirstOrDefault(c => c.Name == "Одежда");
            if (existingCategory == null)
            {
                var expenseCategory = new ExpenseCategory { Description = "", Name = "Одежда", UserId = user.Id, IsBased = true };
                var dbItem = ExpenseCategories.Add(expenseCategory);
                this.SaveChanges();
            }

            existingCategory = ExpenseCategories.FirstOrDefault(c => c.Name == "Такси");
            if (existingCategory == null)
            {
                var expenseCategory = new ExpenseCategory { Description = "", Name = "Такси", UserId = user.Id, IsBased = true };
                var dbItem = ExpenseCategories.Add(expenseCategory);
                this.SaveChanges();
            }

            existingCategory = ExpenseCategories.FirstOrDefault(c => c.Name == "Гигиена");
            if (existingCategory == null)
            {
                var expenseCategory = new ExpenseCategory { Description = "", Name = "Гигиена", UserId = user.Id, IsBased = true };
                var dbItem = ExpenseCategories.Add(expenseCategory);
                this.SaveChanges();
            }

            existingCategory = ExpenseCategories.FirstOrDefault(c => c.Name == "Жилье");
            if (existingCategory == null)
            {
                var expenseCategory = new ExpenseCategory { Description = "", Name = "Жилье", UserId = user.Id, IsBased = true };
                var dbItem = ExpenseCategories.Add(expenseCategory);
                this.SaveChanges();
            }

            existingCategory = ExpenseCategories.FirstOrDefault(c => c.Name == "Машина");
            if (existingCategory == null)
            {
                var expenseCategory = new ExpenseCategory { Description = "", Name = "Машина", UserId = user.Id, IsBased = true };
                var dbItem = ExpenseCategories.Add(expenseCategory);
                this.SaveChanges();
            }

            existingCategory = ExpenseCategories.FirstOrDefault(c => c.Name == "Развлечения");
            if (existingCategory == null)
            {
                var expenseCategory = new ExpenseCategory { Description = "", Name = "Развлечения", UserId = user.Id, IsBased = true };
                var dbItem = ExpenseCategories.Add(expenseCategory);
                this.SaveChanges();
            }

            existingCategory = ExpenseCategories.FirstOrDefault(c => c.Name == "Кафе");
            if (existingCategory == null)
            {
                var expenseCategory = new ExpenseCategory { Description = "", Name = "Кафе", UserId = user.Id, IsBased = true };
                var dbItem = ExpenseCategories.Add(expenseCategory);
                this.SaveChanges();
            }

            var existingICategory = IncomeCategories.FirstOrDefault(c => c.Name == "Депозиты");
            if (existingICategory == null)
            {
                var incomeCategory = new IncomeCategory { Description = "", Name = "Депозиты", UserId = user.Id, IsBased = true };
                var dbItem = IncomeCategories.Add(incomeCategory);
                this.SaveChanges();
            }
            existingICategory = IncomeCategories.FirstOrDefault(c => c.Name == "Зарплата");
            if (existingICategory == null)
            {
                var incomeCategory = new IncomeCategory { Description = "", Name = "Зарплата", UserId = user.Id, IsBased = true };
                var dbItem = IncomeCategories.Add(incomeCategory);
                this.SaveChanges();
            }
            existingICategory = IncomeCategories.FirstOrDefault(c => c.Name == "Сбережения");
            if (existingICategory == null)
            {
                var incomeCategory = new IncomeCategory { Description = "", Name = "Сбережения", UserId = user.Id, IsBased = true };
                var dbItem = IncomeCategories.Add(incomeCategory);
                this.SaveChanges();
            }

            var byn = Currencies.FirstOrDefault(c => c.Code == "BYN");
            if (byn == null)
            {
                byn = new Currency { Code = "BYN", Value = 1 };
                var dbItem = Currencies.Add(byn);
                this.SaveChanges();
            }
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
        public DbSet<MounthLimit> MounthLimits { get; set; }
    }
}
