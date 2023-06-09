﻿using Microsoft.EntityFrameworkCore;
using Shhmoney.Models;

namespace Shhmoney.Data
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private static DbContext instance;

        private DbContext() 
        {
            Database.EnsureCreated();
            Database.OpenConnection();
        }

        public static DbContext GetDbContext()
        {
            return instance ??= new DbContext();
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
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserSession> Sessions { get; set; }
    }
}
