using System;
using System.Collections.Generic;
using Geolocation.Domain.Models;
using Geolocation.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Geolocation.Infrastructure
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<RefreshToken> Tokens { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().OwnsOne(p => p.RefreshTokens);

            builder.Entity<User>().HasData(new List<User>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Password = "pass",
                    Username = "loggin",
                    FirstName = "first",
                    LastName = "user"
                }
            });
        }
    }
}