using Microsoft.Owin;
using Owin;
using SeaBattle.Migrations;
using SeaBattle.Models;
using System.Data.Entity;

[assembly: OwinStartupAttribute(typeof(SeaBattle.Startup))]
namespace SeaBattle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            ConfigureAuth(app);
        }
    }
}
