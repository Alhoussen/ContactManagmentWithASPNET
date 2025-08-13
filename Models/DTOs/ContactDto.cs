namespace ContactManagement.Api.Models.DTOs;

/// <summary>
/// DTO pour la lecture des contacts
/// </summary>
public record ContactDto
{
    /// <summary>
    /// Identifiant unique du contact
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Prénom du contact
    /// </summary>
    public string FirstName { get; init; } = string.Empty;

    /// <summary>
    /// Nom de famille du contact
    /// </summary>
    public string LastName { get; init; } = string.Empty;

    /// <summary>
    /// Nom complet du contact
    /// </summary>
    public string FullName { get; init; } = string.Empty;

    /// <summary>
    /// Adresse email du contact
    /// </summary>
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// Numéro de téléphone du contact
    /// </summary>
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// Adresse du contact
    /// </summary>
    public string? Address { get; init; }

    /// <summary>
    /// Date de création du contact
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Date de dernière mise à jour du contact
    /// </summary>
    public DateTime UpdatedAt { get; init; }


}
