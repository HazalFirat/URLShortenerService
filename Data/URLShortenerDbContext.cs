using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using URLShortenerService.Models;

namespace URLShortenerService.Data
{
    public class URLShortenerDbContext : DbContext
    {
        public URLShortenerDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<URL> URLs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<URL>()
                .Property(x => x.Id);
            modelBuilder.Entity<URL>()
                .Property(x => x.OriginalURL);
            modelBuilder.Entity<URL>()
                .Property(x => x.ShortURL);
            modelBuilder.Entity<URL>()
                .Property(x => x.DateCreated);

            modelBuilder.Entity<URL>()
                .HasIndex(x => x.ShortURL);

            modelBuilder.Entity<URL>()
                .HasIndex(x => new { x.OriginalURL, x.ShortURL }).IsUnique();
        }
    }
}
