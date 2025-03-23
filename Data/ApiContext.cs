using Divination.Models;
using Microsoft.EntityFrameworkCore;

namespace Divination.Data;

public class ApiContext : DbContext
{
    public DbSet<Rule> Rules { get; set; }
    public DbSet<GlossaryTerm> GlossaryTerms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("MtgRules");
    }
}