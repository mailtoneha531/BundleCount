using CountBundle;
using CountBundle.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

class Program
{
    static void Main()
    {

        //Sample data
        using (var context = new BundleCountDbContext())
        {
            var dbContextTransaction = context.Database.BeginTransaction();
            try
            {

                // Create the database and tables
                context.Database.EnsureCreated();

                // Add sample data
                var bike = new BundleEntity
                {
                    Name = "Bike",
                    IsPairExist = false,
                    InventoryCount = int.MaxValue,
                    Parts = new List<BundlePartEntity>
                    {
                        new BundlePartEntity { Name = "Seat", InventoryCount = 4, IsPairExist = false },
                        new BundlePartEntity
                        {
                            Name = "Pedal",
                            InventoryCount = 8,
                            IsPairExist = true
                        },
                        new BundlePartEntity
                        {
                            Name = "Wheel",
                            SubParts = new List<BundlePartSubEntity>
                            {
                                new BundlePartSubEntity { Name = "Frame", InventoryCount = 8 , IsPairExist = true},
                                new BundlePartSubEntity { Name = "Tube", InventoryCount = 8 , IsPairExist = true}
                            }
                        }
                    }
                };

                context.Bundles.Add(bike);

                //foreach (var entry in context.ChangeTracker.Entries())
                //{
                //    Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
                //}

                context.SaveChanges();
                dbContextTransaction.Commit();
            }
            catch (Exception)
            {
                dbContextTransaction.Rollback();
                throw;
            }

            var bikeWithRelatedData = context.Bundles
           .Include(b => b.Parts)
           .ThenInclude(p => p.SubParts)
           .FirstOrDefault(b => b.Name == "Bike");


            int bikesToBuild = CalculateBikes(bikeWithRelatedData);

            //// Calculate and display the maximum number of bikes that can be built
            //int bikesToBuild = CalculateBikes(context.Bundles
            //.Include(b => b.BundleParts)
            //.ThenInclude(p => p.Bundles)
            //.FirstOrDefault(b => b.Name == "Bike"));

            Console.WriteLine($"Maximum number of bikes that can be built: {bikesToBuild}");
        }
    }


    public static int CalculateBikes<T>(T obj)
    {
        int minAvailableCount = int.MaxValue;
        BundleEntity bundle = new BundleEntity();
        BundlePartEntity bundlePart = new BundlePartEntity();
        BundlePartSubEntity bundleSubPart = new BundlePartSubEntity();
        if (obj is BundleEntity bundleObj)
        {
            bundle = bundleObj;
            if (bundle.Parts == null || bundle.Parts.Count == 0)
            {
                return bundle.InventoryCount;
            }
            foreach (var part in bundle.Parts)
            {
                int partAvailableCount = CalculateBikes(part) / (part.IsPairExist ? 2 : 1);
                minAvailableCount = Math.Min(minAvailableCount, partAvailableCount);
            }
        }
        else if (obj is BundlePartEntity bundlePartObj)
        {
            bundlePart = bundlePartObj;
            if (bundlePart.SubParts == null || bundlePart.SubParts.Count == 0)
            {
                return bundlePart.InventoryCount;
            }
            foreach (var part in bundlePart.SubParts)
            {
                int partAvailableCount = CalculateBikes(part) / (part.IsPairExist ? 2 : 1);
                minAvailableCount = Math.Min(minAvailableCount, partAvailableCount);
            }
        }
        else if (obj is BundlePartSubEntity bundleSubPartObj)
        {
            bundleSubPart = bundleSubPartObj;

            return bundleSubPart.InventoryCount;
        }
        return minAvailableCount;
    }
}
