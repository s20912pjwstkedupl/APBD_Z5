using Microsoft.EntityFrameworkCore;

namespace APBD_Z5.Models;

public class SQLDBContext : DbContext
{
    public SQLDBContext(DbContextOptions<SQLDBContext> options) : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<ClientTrip> ClientTrips { get; set; }
    public virtual DbSet<Country> Countries { get; set; }
    public virtual DbSet<CountryTrip> CountryTrips { get; set; }
    public virtual DbSet<Trip> Trips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("trip");
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient)
                .HasName("Client_pk");

            entity.ToTable("Client");

            entity.Property(e => e.IdClient).ValueGeneratedNever();

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(120);

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(120);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(120);

            entity.Property(e => e.Pesel)
                .IsRequired()
                .HasMaxLength(120);

            entity.Property(e => e.Telephone)
                .IsRequired()
                .HasMaxLength(120);
        });

        modelBuilder.Entity<ClientTrip>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.IdTrip })
                .HasName("Client_Trip_pk");
            entity.ToTable("Client_Trip");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.RegisteredAt).HasColumnType("datetime");
            entity.HasOne(d => d.VClient)
                .WithMany(p => p.VClientTrips)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_5_Client");
            entity.HasOne(d => d.VTrip)
                .WithMany(p => p.VClientTrips)
                .HasForeignKey(d => d.IdTrip)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_5_Trip");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.IdCountry)
                .HasName("Country_pk");
            entity.ToTable("Country");
            entity.Property(e => e.IdCountry).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(120);
        });

        modelBuilder.Entity<CountryTrip>(entity =>
        {
            entity.HasKey(e => new { e.IdCountry, e.IdTrip })
                .HasName("Country_Trip_pk");
            entity.ToTable("Country_Trip");
            entity.HasOne(d => d.VCountry)
                .WithMany(p => p.VCountryTrips)
                .HasForeignKey(d => d.IdCountry)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Country_Trip_Country");
            entity.HasOne(d => d.VTrip)
                .WithMany(p => p.VCountryTrips)
                .HasForeignKey(d => d.IdTrip)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Country_Trip_Trip");
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.IdTrip)
                .HasName("Trip_pk");
            entity.ToTable("Trip");
            entity.Property(e => e.IdTrip).ValueGeneratedNever();
            entity.Property(e => e.DateFrom).HasColumnType("datetime");
            entity.Property(e => e.DateTo).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(220);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(120);
        });
        base.OnModelCreating(modelBuilder);
    }
}