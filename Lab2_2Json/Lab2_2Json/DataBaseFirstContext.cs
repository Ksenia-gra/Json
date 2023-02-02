using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab2_2Json;

public partial class DataBaseFirstContext : DbContext
{
    public DataBaseFirstContext()
    {
    }

    public DataBaseFirstContext(DbContextOptions<DataBaseFirstContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auth> Auths { get; set; } = null!;
    public virtual DbSet<Book> Books { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\kseniagrafova\\DataBaseFirst.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auth>(entity =>
        {
            entity.ToTable("auth");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.Property(e => e.Age).HasColumnName("age");

            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasMany(d => d.Books)
                .WithMany(p => p.Auths)
                .UsingEntity<Dictionary<string, object>>(
                    "AuthBook",
                    l => l.HasOne<Book>().WithMany().HasForeignKey("BooksId").OnDelete(DeleteBehavior.ClientCascade),
                    r => r.HasOne<Auth>().WithMany().HasForeignKey("AuthId").OnDelete(DeleteBehavior.ClientCascade),
                    j =>
                    {
                        j.HasKey("AuthId", "BooksId");

                        j.ToTable("auth_book");

                        j.IndexerProperty<long>("AuthId").HasColumnName("auth_id");

                        j.IndexerProperty<long>("BooksId").HasColumnName("books_id");
                    });
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("books");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.Property(e => e.CountPage).HasColumnName("count_page");

            entity.Property(e => e.Price).HasColumnName("price");

            entity.Property(e => e.Title).HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

