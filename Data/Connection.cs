using Microsoft.EntityFrameworkCore;

namespace Data;
public class Connection : DbContext
{
    public DbSet<Client> Clients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;
                                      Initial Catalog=bookServer;
                                      Integrated Security=True;
                                      Trust Server Certificate=True;");
    }

}
