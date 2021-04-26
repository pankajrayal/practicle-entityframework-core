using System.Data.Entity;

namespace EF_Activity01_NetFramework {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext(string connectionString): base(connectionString) {

        }
    }
}