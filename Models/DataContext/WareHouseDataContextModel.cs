using Microsoft.EntityFrameworkCore;

namespace Models.DataContext
{
    class WareHouseDataContextModel : DbContext
    {
        internal string MyConnection { get; set; }
        internal IConfiguration configuration { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InventoryRegister> InventoryRegisters { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WareHouse> WareHouses { get; set; }

        public WareHouseDataContextModel(DbContextOptions<WareHouseDataContextModel> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                GetConnectionString();
                options.UseSqlServer(MyConnection);
            }
        }

        private void GetConnectionString(string connectionString = "DbConection")
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").AddEnvironmentVariables();
            configuration = builder.Build();
            connectionString = configuration.GetConnectionString(connectionString);
        }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            
        }


    }
}