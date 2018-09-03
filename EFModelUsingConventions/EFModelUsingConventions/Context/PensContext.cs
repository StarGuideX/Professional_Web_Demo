using EFModelUsingConventions.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFModelUsingConventions.Context
{
    public class PensContext :DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        private const string ConnectionString = @"server=(localdb)\MSSQLLocalDb;database=WroxBooks;trusted_connection=true";
        /// <summary>
        /// get:允许查询
        /// set:允许添加
        /// </summary>
        public DbSet<Pen> Pens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
