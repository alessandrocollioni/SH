using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace efcoremysql
{
    public partial class MyDbContext : DbContext
    {

        public DbSet<Album> Albuns { get; set; }

        public DbSet<Musica> Musicas { get; set; }

        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=localhost;User Id=root;Password=SLC-sh2019;Database=sistemahospedagemdb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {}

    }
}
