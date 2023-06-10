using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Models.DataBaseContext
{
    public class WareHouseDataContext : DbContext
    {
        public DbSet<Entry>? Entries { get; set; }
        public DbSet<Exit>? Exits { get; set; }
        public DbSet<Group>? Groups { get; set; }
        public DbSet<Inventory>? Inventories { get; set; }
        public DbSet<Photo>? Photos { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<WareHouse>? WareHouses { get; set; }

        public WareHouseDataContext(DbContextOptions<WareHouseDataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Group Admins = new()
            {
                GroupsUserId = 1,
                Name = "Administradores",
                Status = true,
                Level = 1
            };
            Group Seller = new()
            {
                GroupsUserId = 2,
                Name = "Vendedores",
                Status = true,
                Level = 2
            };
            Group Managers = new()
            {
                GroupsUserId = 3,
                Name = "Administrador Inventarios",
                Status = true,
                Level = 2
            };
            User Admin = new()
            {
                GroupsUserId = 1,
                UserId = Admins.GroupsUserId,
                UserName = "admin",
                Name = "",
                LastName = "",
                LastLogin = DateTime.Now,
                Enable = true,
                Password = "admin"
            };
            Product defaultProduct = new(){
                ProductId = 1,
                Name = "Sample",
                Description = "Sample",
                Buy_Price = 1,
                Sell_Price = 2
            };

            Photo defaultphoto = new() {
                PhotoId = 1,
                FilePath= "./photos/default.png",
                ProductId = defaultProduct.ProductId
            };
            modelBuilder.Entity<Product>().HasData(defaultProduct);
            modelBuilder.Entity<Photo>().HasData(defaultphoto);
            modelBuilder.Entity<Group>().HasData(Admins);
            modelBuilder.Entity<Group>().HasData(Seller);
            modelBuilder.Entity<Group>().HasData(Managers);
            modelBuilder.Entity<User>().HasData(Admin);

        }
    }
}