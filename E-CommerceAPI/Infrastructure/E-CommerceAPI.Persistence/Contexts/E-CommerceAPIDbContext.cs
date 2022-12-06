using E_CommerceAPI.Domain.Entities;
using E_CommerceAPI.Domain.Entities.Common;
using E_CommerceAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using File = E_CommerceAPI.Domain.Entities.File;

namespace E_CommerceAPI.Persistence.Contexts
{
    public class E_CommerceAPIDbContext : IdentityDbContext<AppUser, AppRole, string> 
    {
        public E_CommerceAPIDbContext(DbContextOptions options) : base(options){}
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach(var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added=>data.Entity.CreatedDate=DateTime.UtcNow,
                    EntityState.Modified=> data.Entity.UpdatedDate = DateTime.UtcNow,
                    _=>DateTime.UtcNow
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
