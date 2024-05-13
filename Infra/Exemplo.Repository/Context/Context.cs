
using Microsoft.EntityFrameworkCore;
using Exemplo.Domain.Interfaces;
using Exemplo.Repository.Maps;

namespace Exemplo.Repository.Contexts
{
    public class ExemploContext : DbContext, IUnitOfWork<ExemploContext>
    {
        public ExemploContext(DbContextOptions<ExemploContext> options) : base(options) { }
        public int Commit() => this.SaveChanges();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
			
        }
    }
}


