using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class MyDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            String connectionString = "Server=localhost;Database=clients;UID=root;Password=";

            optionBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
