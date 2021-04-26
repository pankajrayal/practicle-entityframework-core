using System;
using Microsoft.EntityFrameworkCore;

namespace EF_Activity001 {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(DbContextOptions options) : base(options) {

        }
    }
}