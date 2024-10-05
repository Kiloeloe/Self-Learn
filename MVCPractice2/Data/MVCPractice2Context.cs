using Microsoft.EntityFrameworkCore;
using MVCPractice2.Models;

namespace MVCPractice2.Data
{
    public class MVCPractice2Context : DbContext
    {
        public MVCPractice2Context(DbContextOptions<MVCPractice2Context>options) : base(options) { }

        //EF Core method to configure relationships between model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //manually defining the many to many relationship
            modelBuilder.Entity<UserItem>().HasKey(ui => new
            {
                ui.ItemId,
                ui.UserId
            });

            //manually defining foreign keys for many to many relationship
            modelBuilder.Entity<UserItem>().HasOne(i => i.Item).WithMany(uc => uc.UserItems).HasForeignKey(i => i.ItemId);
            modelBuilder.Entity<UserItem>().HasOne(u => u.User).WithMany(uc => uc.UserItems).HasForeignKey(u => u.UserId);

            //manually creating data and defining the relationships
            modelBuilder.Entity<Item>().HasData(
                new Item { Id= 4, Name="Microphone",Price= 40, SerialNumberId= 10}
            );

            modelBuilder.Entity<SerialNumber>().HasData(
                new SerialNumber { Id = 10, Name = "MIC150", ItemId = 4 }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics"},
                new Category { Id = 2, Name = "Books"}
            );
            base.OnModelCreating(modelBuilder);
        }

        //model refferences
        public DbSet<Item> Items { get; set; }
        public DbSet<SerialNumber> SerialNumber { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<UserItem> UserItems { get; set; }
    }
}
