using ContactManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManagement.Api.Data;

/// <summary>
/// Contexte de base de données pour la gestion des contacts
/// </summary>
public class ContactManagementDbContext : DbContext
{
    public ContactManagementDbContext(DbContextOptions<ContactManagementDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Collection des contacts
    /// </summary>
    public DbSet<Contact> Contacts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration de l'entité Contact
        modelBuilder.Entity<Contact>(entity =>
        {
            // Clé primaire
            entity.HasKey(e => e.Id);

            // Configuration des propriétés
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20);

            entity.Property(e => e.Address)
                .HasMaxLength(200);

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();



            // Index pour améliorer les performances de recherche
            entity.HasIndex(e => e.Email)
                .IsUnique()
                .HasDatabaseName("IX_Contact_Email");

            entity.HasIndex(e => new { e.FirstName, e.LastName })
                .HasDatabaseName("IX_Contact_Name");

            entity.HasIndex(e => e.CreatedAt)
                .HasDatabaseName("IX_Contact_CreatedAt");
        });

        // Données de test (seed data)
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        var contacts = new[]
        {
            new Contact
            {
                Id = 1,
                FirstName = "Alhassane",
                LastName = "TRAORE",
                Email = "alh@email.com",
                PhoneNumber = "75632299",
                Address = "Djana",
                CreatedAt = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc)
            },
            new Contact
            {
                Id = 2,
                FirstName = "Mariam",
                LastName = "CISSE",
                Email = "mcisse@email.com",
                PhoneNumber = "987654321",
                Address = "79653399",
                CreatedAt = new DateTime(2024, 1, 10, 12, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 10, 12, 0, 0, DateTimeKind.Utc)
            },
            new Contact
            {
                Id = 3,
                FirstName = "Salimata",
                LastName = "SIDIBE",
                Email = "sali@email.com",
                PhoneNumber = "0147258369",
                CreatedAt = new DateTime(2024, 1, 20, 12, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2024, 1, 20, 12, 0, 0, DateTimeKind.Utc)
            }
        };

        modelBuilder.Entity<Contact>().HasData(contacts);
    }
}

