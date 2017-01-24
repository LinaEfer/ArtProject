using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ArtProject.Models;


namespace ArtProject.DAL
{
    public class LibraryContext : DbContext
    {
        public DbSet<Painter> Painters { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}