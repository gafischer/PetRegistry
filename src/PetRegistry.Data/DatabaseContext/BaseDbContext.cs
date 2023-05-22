using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PetRegistry.Data.Entities;

namespace PetRegistry.Data.DatabaseContext
{
    [ExcludeFromCodeCoverage]
    public class BaseDbContext : DbContext
    {
        public BaseDbContext()
        { 
        }

        public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options)
        { 
        }

        public virtual DbSet<PetEntity> Pets { get; set; }
    }
}
