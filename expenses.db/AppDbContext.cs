﻿using Microsoft.EntityFrameworkCore;

namespace expenses.db
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
