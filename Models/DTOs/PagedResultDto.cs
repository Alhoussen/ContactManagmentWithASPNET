namespace ContactManagement.Api.Models.DTOs;

/// <summary>
/// DTO générique pour les résultats paginés
/// </summary>
/// <typeparam name="T">Type des éléments dans la page</typeparam>
public record PagedResultDto<T>
{
    /// <summary>
    /// Liste des éléments de la page courante
    /// </summary>
    public IEnumerable<T> Items { get; init; } = Enumerable.Empty<T>();

    /// <summary>
    /// Numéro de la page courante
    /// </summary>
    public int PageNumber { get; init; }

    /// <summary>
    /// Nombre d'éléments par page
    /// </summary>
    public int PageSize { get; init; }

    /// <summary>
    /// Nombre total d'éléments
    /// </summary>
    public int TotalCount { get; init; }

    /// <summary>
    /// Nombre total de pages
    /// </summary>
    public int TotalPages { get; init; }

    /// <summary>
    /// Indique s'il y a une page précédente
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    /// Indique s'il y a une page suivante
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;
}

