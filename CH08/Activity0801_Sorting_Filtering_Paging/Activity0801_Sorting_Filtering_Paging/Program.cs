using EF_Activity001.Models;
using InventoryHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Activity0801_Sorting_Filtering_Paging {
    class Program {
        private static IConfigurationRoot _configuration;
        private static DbContextOptionsBuilder<AdventureWorksContext> _optionsBuilder;
        static void Main(string[] args) {
            BuildOptions();
            Console.WriteLine("List People then Order and Take");
            ListPeopleThenOrderAndTake();
            Console.WriteLine("Query People, order, then list and take");
            QueryPeopleOrderedToListAndTake();

            Console.WriteLine("Please Enter the partial First or Last Name, or the Person Type to search for:");
            var result = Console.ReadLine();
            FilteredPeople(result);

            Console.Clear();
            Console.WriteLine("Please Enter the partial First or Last Name, or the Person Type to search for:");
            int pageSize = 10;
            for (int pageNumber = 0; pageNumber < 10; pageNumber++) {
                Console.WriteLine($"Page: {pageNumber}");
                FilteredAndPagedResult(result, pageNumber, pageSize);
            }
        }
        static void BuildOptions() {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<AdventureWorksContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("AdventureWorks"));
        }
        static void ListPeopleThenOrderAndTake() {
            using (var db = new AdventureWorksContext(_optionsBuilder.Options)) {
                var people = db.People.ToList().OrderByDescending(x => x.LastName);
                foreach(var person in people.Take(10)) {
                    Console.WriteLine($"{person.FirstName} {person.LastName}");
                }
            }
        }
        static void QueryPeopleOrderedToListAndTake() {
            using (var db = new AdventureWorksContext(_optionsBuilder.Options)) {
                var query = db.People.OrderByDescending(x => x.LastName);
                var result = query.Take(10);
                foreach (var person in result) {
                    Console.WriteLine($"{person.FirstName} {person.LastName}");
                }
            }
        }
        static void FilteredPeople(string filter) {
            using (var db = new AdventureWorksContext(_optionsBuilder.Options)) {
                var searchTerm = filter.ToLower();
                var query = db.People.Where(x =>
                    x.LastName.ToLower().Contains(searchTerm) ||
                    x.FirstName.ToLower().Contains(searchTerm) ||
                    x.PersonType.ToLower().Equals(searchTerm)
                    );

                foreach (var person in query) {
                    Console.WriteLine($"{person.FirstName} {person.LastName}, {person.PersonType}");
                }
            }
        }

        static void FilteredAndPagedResult(string filter, int pageNumber, int pageSize) {
            using (var db = new AdventureWorksContext(_optionsBuilder.Options)) {
                var searchTerm = filter.ToLower();
                var query = db.People.Where(x =>
                    x.LastName.ToLower().Contains(searchTerm) ||
                    x.FirstName.ToLower().Contains(searchTerm) ||
                    x.PersonType.ToLower().Equals(searchTerm))
                    .OrderBy(x => x.LastName)
                    .Skip(pageNumber * pageSize)
                    .Take(pageSize);
                foreach (var person in query) {
                    Console.WriteLine($"\t{person.FirstName} {person.LastName}, {person.PersonType}");
                }
            }

            var salesReportDetails = db.SalesPerson.Select(sp => new { 
                beid = sp.BusinessEntityId,
                sp.BusinessEntity.BusinessEntity.FirstName,
                sp.BusinessEntity.BusinessEntity.LastName,
                sp.SalesYtd,
                Territories = sp.SalesTerritoryHistory.Select(y => y.Territory.Name),
                OrderCount = sp.SalesOrderHeader.Count(),
                TotalProductsSold = sp.SalesOrderHeader.SelectMany(y => y.SalesOrderDetail).Sum(z => z.OrderQty)
            }).Where(srds => srds.SalesYtd > filter).AsQueryable()
            .OrderBy(srds => srds.LastName).ThenBy(srds => srds.FirstName).ThenByDescending(srds => srds.SalesYtd)
            .Take(20).ToList();

        }
    }
}

