using AllrideApiRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;



namespace AllrideApi.Data
{
    public class ContextFactory : IDesignTimeDbContextFactory<AllrideApiDbContext>
    {
        public AllrideApiDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<AllrideApiDbContext>();
            var connectionString = configuration.GetConnectionString("SqlConnection");
            builder.UseNpgsql(connectionString, option => {
                option.UseNetTopologySuite();
                option.MigrationsAssembly(Assembly.GetAssembly(typeof(AllrideApiDbContext)).GetName().Name);
            });



            return new AllrideApiDbContext(builder.Options);
        }
    }
}


