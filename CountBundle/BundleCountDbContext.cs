using CountBundle.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountBundle
{
    public class BundleCountDbContext : DbContext
    {
        public DbSet<BundleEntity> Bundles { get; set; }
        public DbSet<BundlePartEntity> BundleParts { get; set; }
        public DbSet<BundlePartSubEntity> BundlePartSubEntity { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Enable logging to the console
            //optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Debug)));

            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CountBundleDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BundleEntity>()
                .HasKey(b => b.BundleEntityId);

            modelBuilder.Entity<BundlePartEntity>()
                .HasKey(bp => bp.BundlePartEntityId);

            modelBuilder.Entity<BundlePartSubEntity>()
                .HasKey(bp => bp.BundleSubEntityId);


            modelBuilder.Entity<BundleEntity>()
                .HasMany(b => b.Parts)
                .WithOne(p => p.BundleEntity)
                .HasForeignKey(p => p.BundleEntityId);

            modelBuilder.Entity<BundlePartEntity>()
               .HasMany(b => b.SubParts)
               .WithOne(p => p.BundlePartEntity)
               .HasForeignKey(p => p.BundlePartEntityId);
        }
        public int CalculateMaxFinishedBundles(BundleEntity rootBundle)
        {
            Dictionary<string, int> inventory = new Dictionary<string, int>();
            return CalculateMaxFinishedBundles(rootBundle, inventory);
        }
        private int CalculateMaxFinishedBundles(BundleEntity bundle, Dictionary<string, int> inventory)
        {
            if (bundle.Parts.Count == 0) // Leaf node
            {
                if (inventory.ContainsKey(bundle.Name) && inventory[bundle.Name] > 0)
                {
                    inventory[bundle.Name]--;
                    return 1;
                }
                return 0;
            }

            int maxBundles = int.MaxValue;
            foreach (var part in bundle.Parts)
            {
                maxBundles = Math.Min(maxBundles, CalculateMaxFinishedBundles(part.BundleEntity, inventory));
            }

            if (maxBundles > 0)
            {
                foreach (var part in bundle.Parts)
                {
                    inventory[part.BundleEntity.Name] -= maxBundles * part.InventoryCount;
                }
            }

            return maxBundles;
        }
    }
}