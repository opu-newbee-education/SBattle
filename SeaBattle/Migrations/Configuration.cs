namespace SeaBattle.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    internal sealed class Configuration : DbMigrationsConfiguration<SeaBattle.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SeaBattle.Models.ApplicationDbContext";
        }

        protected override void Seed(SeaBattle.Models.ApplicationDbContext context)
        {
            return;


            var _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            assemblyFolder = @"C:\Users\humanoid\Desktop\SeaBattle\SeaBattle\Migrations\";
            context.Database.BeginTransaction();

            var userLines = File.ReadAllLines(assemblyFolder + "data/users.csv");

            foreach (var line in userLines)
            {
                var split = line.Split('|');
                var user = new ApplicationUser { UserName = split[0], Email = split[0] };
                var result = _userManager.Create(user, split[1]);
            }
            context.Database.CurrentTransaction.Commit();


            var userIds = context.Users.Select(x => x.Id).ToArray();



            var statsLines = File.ReadAllLines(assemblyFolder + "data/userStats.csv");


            context.Database.BeginTransaction();
            for (int i = 0; i < userIds.Length; i++)
            {
                if (i >= statsLines.Length)
                {
                    break;
                }
                var sep = statsLines[i].Split('|');

                context.UserStats.Add(new Models.UserStats()
                {
                    UserId = userIds[i],
                    GamesPlayed = int.Parse(sep[0]),
                    GamesWon = (int)float.Parse(sep[1]),
                    TotalPlayTime = int.Parse(sep[2]),
                    LongestTimePlayed = int.Parse(sep[3]),
                    ShortestTimePlayed = int.Parse(sep[4])
                });


            }

            context.Database.CurrentTransaction.Commit();


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
