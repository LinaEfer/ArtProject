namespace ArtProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ArtProject.Models;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    internal sealed class Configuration : DbMigrationsConfiguration<ArtProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ArtProject.Models.ApplicationDbContext context)
        {
            if (!context.Users.Any(u => u.UserName == "balou@mail.com"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "balou@mail.com" };

                userManager.Create(user, "password");
                roleManager.Create(new IdentityRole { Name = "admin" });
                userManager.AddToRole(user.Id, "admin");
            }

            /*  var painters = new List<Painter>
              {
                  new Painter { FirstName = "Клод",   LastName = "Моне" },
                  new Painter { FirstName = "Иван", LastName = "Айвазовский"},
              };
              painters.ForEach(s => context.Painters.AddOrUpdate(p => p.LastName, s));
              context.SaveChanges();

              var pictures = new List<Picture>
              {
                  new Picture {
                      PainterID = painters.Single(s => s.LastName == "Айвазовский").PainterID,
                      Title = "Берег моря ночью. У маяка",
                      RealiseDate = DateTime.Parse("1837-09-01"),
                      Description = "	Холст, масло"},
                   new Picture {
                      PainterID = painters.Single(s => s.LastName == "Моне").PainterID,
                      Title = "Дама в саду",
                      RealiseDate = DateTime.Parse("1866-09-01"),
                      Description = "	Холст, масло"}
              };

              foreach (Picture e in pictures)
              {
                  var pictureInDataBase = context.Pictures.Where(
                      s =>
                           s.Painter.PainterID == e.PainterID).SingleOrDefault();
                  if (pictureInDataBase == null)
                  {
                      context.Pictures.Add(e);
                  }
              }
              context.SaveChanges(); */

        }
    } 
}
