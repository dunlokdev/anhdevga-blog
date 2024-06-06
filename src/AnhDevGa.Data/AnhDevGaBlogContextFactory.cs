using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhDevGa.Data
{
    public class AnhDevGaBlogContextFactory : IDesignTimeDbContextFactory<AnhDevGaContext>
    {
        public AnhDevGaContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AnhDevGaContext>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); 

            return new AnhDevGaContext(optionsBuilder.Options);
        }
    }
}
