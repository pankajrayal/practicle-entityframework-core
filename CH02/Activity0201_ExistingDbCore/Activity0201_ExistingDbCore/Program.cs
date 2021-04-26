using EF_Activity001.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace Activity0201_ExistingDbCore {
    class Program {
        static IConfigurationRoot _configuration;
        static DbContextOptionsBuilder<AdventureWorksContext> _optionsBuilder;
        static void Main(string[] args) {
            BuildConfiguration();
            BuildOptions();
            ListPeople();
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }

        static void BuildConfiguration() {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();
        }

        static void BuildOptions() {
            _optionsBuilder = new DbContextOptionsBuilder<AdventureWorksContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("AdventureWorks"));
        }

        static void ListPeople() {
            using (var db = new AdventureWorksContext(_optionsBuilder.Options)) {
                var people = db.People.OrderByDescending(x => x.LastName).Take(20).ToList();
                foreach (var person in people) {
                    Console.WriteLine($"{person.FirstName} {person.LastName}");
                }
            }
        }
    }
}