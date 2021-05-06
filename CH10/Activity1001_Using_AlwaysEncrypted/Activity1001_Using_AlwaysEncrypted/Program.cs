using AutoMapper;
using InventoryDatabaseCore;
using InventoryHelpers;
using InventoryModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using InventoryModels.Dtos;
using AutoMapper.QueryableExtensions;

namespace Activity1001_Using_AlwaysEncrypted {
    class Program {
        static IConfigurationRoot _configuration;
        static DbContextOptionsBuilder<InventoryDbContext> _optionsBuilder;
        private static MapperConfiguration _mapperConfig;
        private static IMapper _mapper;
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args) {
            BuildOptions();
            BuildMapper();
            //DeleteAllItems();
            //InsertItems();
            //UpdateItems();
            //ListInventory();
            GetItemsForListing();
            GetItemsForListingWithParams();
            GetItemsForListingLinq();
            //ListInventoryWithProjection();
            ListCategoriesAndColors();
            ListInventoryWithAlwaysEncrypted();
        }

        static void BuildOptions() {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("InventoryManager"));
        }
        static void DeleteAllItems() {
            using (var db = new InventoryDbContext(_optionsBuilder.Options)) {
                var items = db.Items.ToList();
                foreach (var item in items) {
                    item.LastModifiedUserId = 1;
                }
                db.Items.RemoveRange(items);
                db.SaveChanges();
            }
        }
        static void ListInventory() {
            using (var db = new InventoryDbContext(_optionsBuilder.Options)) {
                var items = db.Items.Take(5).OrderBy(x => x.Name).ToList();
                var result = _mapper.Map<List<Item>, List<ItemDto>>(items);
                result.ForEach(x => Console.WriteLine($"New Item: {x}"));
            }
        }
        static void GetItemsForListing() {
            using (var db = new InventoryDbContext(_optionsBuilder.Options)) {
                var results = db.ItemsForListing.FromSqlRaw("EXECUTE dbo.GetItemsForListing").ToList();
                foreach (var item in results) {
                    Console.WriteLine($"ITEM {item.Name} {item.Description}");
                }
            }
        }
        static void GetItemsForListingWithParams() {
            var minDate = new SqlParameter("minDate", new DateTime(2021, 5, 1));
            var maxDate = new SqlParameter("maxDate", new DateTime(2022, 5, 1));
            Console.WriteLine("Listing items using stored procedures:");
            using (var db = new InventoryDbContext(_optionsBuilder.Options)) {
                var results = db.ItemsForListing
                    .FromSqlRaw("EXECUTE dbo.GetItemsForListing @minDate, @maxDate", minDate, maxDate)
                    .ToList();
                foreach (var item in results) {
                    Console.WriteLine($"ITEM {item.Name} - {item.Description}");
                }
            }
        }
        static void InsertItems() {
            var items = new List<Item>() {
                new Item() { Name = "Top Gun", IsActive = true, Description="I feel the need, the need for speed" },
                new Item() { Name = "Batman Begins", IsActive = true,
                    Description="You either die the hero or live long enough to see yourself become the villain"},
                new Item() { Name = "Inception", IsActive = true, Description="You mustn't be afraid to dream a little bigger" },
                new Item() { Name = "Star Wars: The Empire Strikes Back", IsActive = true, Description="He will join us or die, master"},
                new Item() { Name = "Remember the Titans", IsActive = true, Description = "Attitude reflects leadership"}
            };

            using (var db = new InventoryDbContext(_optionsBuilder.Options)) {
                foreach (var item in items) {
                    item.LastModifiedUserId = 1;
                }
                db.AddRange(items);
                db.SaveChanges();
            }
        }
        static void UpdateItems() {
            using (var db = new InventoryDbContext(_optionsBuilder.Options)) {
                var items = db.Items.ToList();
                foreach (var item in items) {
                    item.LastModifiedUserId = 1;
                    item.CurrentOrFinalPrice = 9.99M;
                }
                db.Items.UpdateRange(items);
                db.SaveChanges();
            }
        }
        static void BuildMapper() {
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(InventoryMapper));
            _serviceProvider = services.BuildServiceProvider();

            _mapperConfig = new MapperConfiguration(cfg => {
                cfg.AddProfile<InventoryMapper>();
            });
            _mapperConfig.AssertConfigurationIsValid();
            _mapper = _mapperConfig.CreateMapper();
        }
        //static void GetItemsForListingLinq() {
        //    Console.Clear();
        //    var minDateValue = new DateTime(2020, 06, 01);
        //    var maxDateValue = new DateTime(2021, 06, 01);

        //    using (var db = new InventoryDbContext(_optionsBuilder.Options)) {
        //        var results = db.Items.Select(x => new GetItemsForListingWithDateDto {
        //            CreatedDate = x.CreatedDate,
        //            CategoryName = x.Category.Name,
        //            Description = x.Description,
        //            IsActive = x.IsActive,
        //            IsDeleted = x.IsDeleted,
        //            Name = x.Name,
        //            Notes = x.Notes
        //        }).Where(x => x.CreatedDate >= minDateValue && x.CreatedDate <= maxDateValue)
        //        .OrderBy(y => y.CategoryName).ThenBy(z => z.Name).ToList();

        //        foreach (var item in results) {
        //            Console.WriteLine($"ITEM {item.CategoryName} | {item.Name} - {item.Description}");
        //        }
        //    }
        //}
        static void GetItemsForListingLinq() {
            Console.Clear();
            var minDateValue = new DateTime(2021, 01, 01);
            var maxDateValue = new DateTime(2022, 01, 01);
            using (var db = new InventoryDbContext(_optionsBuilder.Options)) {
                var results = db.Items.Include(x => x.Category).ToList()
                    .Select(x => new GetItemsForListingWithDateDto {
                        CreatedDate = x.CreatedDate,
                        CategoryName = x.Category.Name,
                        Description = x.Description,
                        IsActive = x.IsActive,
                        IsDeleted = x.IsDeleted,
                        Name = x.Name,
                        Notes = x.Notes
                    }).Where(x => x.CreatedDate >= minDateValue && x.CreatedDate <= maxDateValue)
                    .OrderBy(y => y.CategoryName).ThenBy(z => z.Name).ToList();
                foreach (var item in results) {
                    Console.WriteLine($"ITEM: {item.CategoryName}| {item.Name} - {item.Description}");
                }
            }
        }
        //static void ListInventoryWithProjection() {
        //    using (var db = new InventoryDbContext(_optionsBuilder.Options)) {
        //        var items = db.Items.Take(5)
        //            .OrderBy(x => x.Name)
        //            .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
        //            .ToList();
        //        items.ForEach(x => Console.WriteLine($"New Item: {x}"));
        //    }
        //}
        static void ListInventoryWithProjection() {
            using (var db = new InventoryDbContext(_optionsBuilder.Options)) {
                var items = db.Items.OrderBy(x => x.Name).Take(5)
                    .Select(x => new ItemDto {
                        Name = x.Name,
                        Description = x.Description
                    }).ToList();
                items.ForEach(x => Console.WriteLine($"New Item: {x}"));
            }
        }
        static void ListCategoriesAndColors() {
            using (var db = new InventoryDbContext(_optionsBuilder.Options)) {
                //var results = db.Categories
                //    .Include(x => x.CategoryColor)
                //    .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).ToList();
                //foreach (var c in results) {
                //    Console.WriteLine($"{c.Category} | {c.CategoryColor.Color}");
                //}

                var results = db.Categories
                    .Select(x => new CategoryDto { 
                        Category = x.Name,
                        CategoryColor = new CategoryColorDto { Color = x.CategoryColor.ColorValue}
                    });
            }
        }
        static void ListInventoryWithAlwaysEncrypted() {
            using (var db = new InventoryDbContext(_optionsBuilder.Options)) {
                Console.WriteLine("-------------------");
                var theItems = db.Items.ToList().OrderBy(x => x.Name).Take(5);
                var items = _mapper.Map<List<ItemDto>>(theItems);
                items.ForEach(x => Console.WriteLine($"New Item: {x}"));
            }
        }
    }
}