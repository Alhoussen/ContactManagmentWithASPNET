using System.ComponentModel.DataAnnotations;

namespace ContactManagement.Api.Models;

/// <summary>
/// Représente un contact dans le système de gestion
/// </summary>
public class Contact
{
    /// <summary>
    /// Identifiant unique du contact
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Prénom du contact
    /// </summary>
    [Required(ErrorMessage = "Le prénom est obligatoire")]
    [StringLength(50, ErrorMessage = "Le prénom ne peut pas dépasser 50 caractères")]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Nom de famille du contact
    /// </summary>
    [Required(ErrorMessage = "Le nom de famille est obligatoire")]
    [StringLength(50, ErrorMessage = "Le nom de famille ne peut pas dépasser 50 caractères")]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Adresse email du contact
    /// </summary>
    [Required(ErrorMessage = "L'email est obligatoire")]
    [EmailAddress(ErrorMessage = "L'adresse email n'est pas valide")]
    [StringLength(100, ErrorMessage = "L'email ne peut pas dépasser 100 caractères")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Numéro de téléphone du contact
    /// </summary>
    [Phone(ErrorMessage = "Le numéro de téléphone n'est pas valide")]
    [StringLength(20, ErrorMessage = "Le numéro de téléphone ne peut pas dépasser 20 caractères")]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Adresse du contact
    /// </summary>
    [StringLength(200, ErrorMessage = "L'adresse ne peut pas dépasser 200 caractères")]
    public string? Address { get; set; }

    /// <summary>
    /// Date de création du contact
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date de dernière mise à jour du contact
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;



    /// <summary>
    /// Nom complet du contact (propriété calculée)
    /// </summary>
    public string FullName => $"{FirstName} {LastName}";
}
