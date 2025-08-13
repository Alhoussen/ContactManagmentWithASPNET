namespace ContactManagement.Api.Models.DTOs;

/// <summary>
/// DTO pour les paramètres de recherche et pagination des contacts
/// </summary>
public record ContactSearchDto
{
    /// <summary>
    /// Terme de recherche pour filtrer par nom, prénom ou email
    /// </summary>
    public string? SearchTerm { get; init; }

    /// <summary>
    /// Numéro de page (commence à 1)
    /// </summary>
    public int PageNumber { get; init; } = 1;

    /// <summary>
    /// Nombre d'éléments par page (max 100)
    /// </summary>
    public int PageSize { get; init; } = 10;

    /// <summary>
    /// Champ de tri (FirstName, LastName, Email, CreatedAt)
    /// </summary>
    public string SortBy { get; init; } = "LastName";

    /// <summary>
    /// Ordre de tri (asc ou desc)
    /// </summary>
    public string SortOrder { get; init; } = "asc";
}

