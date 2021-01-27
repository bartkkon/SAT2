using Microsoft.EntityFrameworkCore;
using Saving_Accelerator_Tool2.StandardCost.Connection;
using Saving_Accelerator_Tool2.StandardCost.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tool2.StandardCost.Data
{
    public class DBConnectionContext_StandardCost : DbContext
    {
        public DbSet<StandardCost_DB> StandardCost { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.connectionString);
        }

    }
}
