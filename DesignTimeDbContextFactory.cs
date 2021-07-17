using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NetScape.Core;
using NetScape.Modules.DAL;

namespace ANewServer
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext<MyPlayer>>
    {
        public DatabaseContext<MyPlayer> CreateDbContext(string[] args)
        {
            var configRoot = ServerHandler.CreateConfigurationRoot("appsettings.json");
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext<MyPlayer>>();
            optionsBuilder.UseNpgsql(configRoot.GetConnectionString("NetScape"),
                 x => x.MigrationsAssembly(typeof(Program)
                    .Assembly.GetName().Name));
            return new DatabaseContext<MyPlayer>(optionsBuilder.Options);
        }
    }
}
