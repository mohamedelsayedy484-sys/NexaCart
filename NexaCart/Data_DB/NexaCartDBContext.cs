using Microsoft.EntityFrameworkCore;
using NexaCart.Models;

namespace NexaCart.Data_DB
{
    public class NexaCartDBContext:DbContext
    {
        public NexaCartDBContext(DbContextOptions<NexaCartDBContext>options) :base(options)
        {
            
        }
       public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }

        //*********************** Fluent API************************

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Description)
                    .HasMaxLength(500);

                entity.Property(p => p.Price)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(p => p.Stock)
                    .IsRequired();

                entity.Property(p => p.ImageUrl)
                    .HasMaxLength(500);

                entity.HasOne(p => p.Category)
                    .WithMany(c => c.products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }



    }
}
