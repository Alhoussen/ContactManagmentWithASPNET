using System.ComponentModel.DataAnnotations;

namespace ContactManagement.Api.Models.DTOs;

/// <summary>
/// DTO pour la création d'un nouveau contact
/// </summary>
public record CreateContactDto
{
    /// <summary>
    /// Prénom du contact
    /// </summary>
    [Required(ErrorMessage = "Le prénom est obligatoire")]
    [StringLength(50, ErrorMessage = "Le prénom ne peut pas dépasser 50 caractères")]
    public string FirstName { get; init; } = string.Empty;

    /// <summary>
    /// Nom de famille du contact
    /// </summary>
    [Required(ErrorMessage = "Le nom de famille est obligatoire")]
    [StringLength(50, ErrorMessage = "Le nom de famille ne peut pas dépasser 50 caractères")]
    public string LastName { get; init; } = string.Empty;

    /// <summary>
    /// Adresse email du contact
    /// </summary>
    [Required(ErrorMessage = "L'email est obligatoire")]
    [EmailAddress(ErrorMessage = "L'adresse email n'est pas valide")]
    [StringLength(100, ErrorMessage = "L'email ne peut pas dépasser 100 caractères")]
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// Numéro de téléphone du contact
    /// </summary>
    [Phone(ErrorMessage = "Le numéro de téléphone n'est pas valide")]
    [StringLength(20, ErrorMessage = "Le numéro de téléphone ne peut pas dépasser 20 caractères")]
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// Adresse du contact
    /// </summary>
    [StringLength(200, ErrorMessage = "L'adresse ne peut pas dépasser 200 caractères")]
    public string? Address { get; init; }


}
