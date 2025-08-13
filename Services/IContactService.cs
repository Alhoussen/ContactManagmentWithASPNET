using ContactManagement.Api.Models.DTOs;

namespace ContactManagement.Api.Services;

/// <summary>
/// Interface pour le service de gestion des contacts
/// </summary>
public interface IContactService
{
    /// <summary>
    /// Récupère tous les contacts avec pagination et recherche
    /// </summary>
    /// <param name="searchDto">Paramètres de recherche et pagination</param>
    /// <param name="cancellationToken">Token d'annulation</param>
    /// <returns>Résultat paginé des contacts</returns>
    Task<PagedResultDto<ContactDto>> GetContactsAsync(ContactSearchDto searchDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Récupère un contact par son identifiant
    /// </summary>
    /// <param name="id">Identifiant du contact</param>
    /// <param name="cancellationToken">Token d'annulation</param>
    /// <returns>Le contact s'il existe, null sinon</returns>
    Task<ContactDto?> GetContactByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Crée un nouveau contact
    /// </summary>
    /// <param name="createContactDto">Données du contact à créer</param>
    /// <param name="cancellationToken">Token d'annulation</param>
    /// <returns>Le contact créé</returns>
    Task<ContactDto> CreateContactAsync(CreateContactDto createContactDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Met à jour un contact existant
    /// </summary>
    /// <param name="id">Identifiant du contact à mettre à jour</param>
    /// <param name="updateContactDto">Nouvelles données du contact</param>
    /// <param name="cancellationToken">Token d'annulation</param>
    /// <returns>Le contact mis à jour s'il existe, null sinon</returns>
    Task<ContactDto?> UpdateContactAsync(int id, UpdateContactDto updateContactDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Supprime un contact
    /// </summary>
    /// <param name="id">Identifiant du contact à supprimer</param>
    /// <param name="cancellationToken">Token d'annulation</param>
    /// <returns>True si le contact a été supprimé, false s'il n'existait pas</returns>
    Task<bool> DeleteContactAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Vérifie si un email est déjà utilisé par un autre contact
    /// </summary>
    /// <param name="email">Email à vérifier</param>
    /// <param name="excludeContactId">ID du contact à exclure de la vérification (pour les mises à jour)</param>
    /// <param name="cancellationToken">Token d'annulation</param>
    /// <returns>True si l'email existe déjà, false sinon</returns>
    Task<bool> IsEmailExistsAsync(string email, int? excludeContactId = null, CancellationToken cancellationToken = default);


}

