
using API.Controllers;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data

{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CategoryOne> CategoryOne { get; set; }
        public DbSet<CategoryTwo> CategoryTwo { get; set; }
        public DbSet<CategoryThree> CategoryThree { get; set; }
    }
}