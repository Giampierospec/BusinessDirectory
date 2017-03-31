using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDirectory.Models
{
    public class BusinessDbContext: DbContext
    {
        private IConfigurationRoot _config;

        public BusinessDbContext(IConfigurationRoot config, DbContextOptions options):base(options)
        {
            _config = config;
        }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:BusinessDirectoryConnections"]);
        }
    }
}
