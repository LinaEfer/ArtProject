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
    internal sealed class Configuration : DbMigrationsConfiguration<ArtProject.DAL.LibraryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        bool AddUserAndRole(ArtProject.Models.ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("canEdit"));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "user1@contoso.com",
            };
            ir = um.Create(user, "P_assw0rd1");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "canEdit");
            return ir.Succeeded;
        }

        protected override void Seed(DAL.LibraryContext context)
        {
            var painters = new List<Painter>
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
            context.SaveChanges();

        }
    }
}
