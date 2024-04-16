﻿using APIProject.Entities;
using Microsoft.EntityFrameworkCore;
namespace APIProject.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Employee> employees { get; set; }
    }
}
