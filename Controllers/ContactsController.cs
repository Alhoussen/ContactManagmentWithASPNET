using ContactManagement.Api.Models.DTOs;
using ContactManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagement.Api.Controllers;

/// <summary>
/// Contrôleur pour la gestion des contacts
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;
    private readonly ILogger<ContactsController> _logger;

    public ContactsController(IContactService contactService, ILogger<ContactsController> logger)
    {
        _contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Récupère tous les contacts avec pagination et recherche
    /// </summary>
    /// <param name="searchDto">Paramètres de recherche et pagination</param>
    /// <param name="cancellationToken">Token d'annulation</param>
    /// <returns>Liste paginée des contacts</returns>
    /// <response code="200">Retourne la liste des contacts</response>
    /// <response code="400">Paramètres de requête invalides</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResultDto<ContactDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResultDto<ContactDto>>> GetContacts(
        [FromQuery] ContactSearchDto searchDto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Validation des paramètres
            if (searchDto.PageNumber < 1)
            {
                return BadRequest("Le numéro de page doit être supérieur à 0.");
            }

            if (searchDto.PageSize < 1 || searchDto.PageSize > 100)
            {
                return BadRequest("La taille de page doit être entre 1 et 100.");
            }

            var result = await _contactService.GetContactsAsync(searchDto, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération des contacts");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Une erreur interne s'est produite lors de la récupération des contacts.");
        }
    }

    /// <summary>
    /// Récupère un contact par son identifiant
    /// </summary>
    /// <param name="id">Identifiant du contact</param>
    /// <param name="cancellationToken">Token d'annulation</param>
    /// <returns>Le contact demandé</returns>
    /// <response code="200">Retourne le contact</response>
    /// <response code="404">Contact non trouvé</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ContactDto>> GetContact(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var contact = await _contactService.GetContactByIdAsync(id, cancellationToken);

            if (contact == null)
            {
                return NotFound($"Le contact avec l'ID {id} n'a pas été trouvé.");
            }

            return Ok(contact);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la récupération du contact {ContactId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Une erreur interne s'est produite lors de la récupération du contact.");
        }
    }

    /// <summary>
    /// Crée un nouveau contact
    /// </summary>
    /// <param name="createContactDto">Données du contact à créer</param>
    /// <param name="cancellationToken">Token d'annulation</param>
    /// <returns>Le contact créé</returns>
    /// <response code="201">Contact créé avec succès</response>
    /// <response code="400">Données invalides</response>
    /// <response code="409">Email déjà utilisé</response>
    [HttpPost]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ContactDto>> CreateContact(
        [FromBody] CreateContactDto createContactDto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdContact = await _contactService.CreateContactAsync(createContactDto, cancellationToken);

            return CreatedAtAction(
                nameof(GetContact),
                new { id = createdContact.Id },
                createdContact);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Tentative de création d'un contact avec un email existant");
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la création du contact");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Une erreur interne s'est produite lors de la création du contact.");
        }
    }

    /// <summary>
    /// Met à jour un contact existant
    /// </summary>
    /// <param name="id">Identifiant du contact à mettre à jour</param>
    /// <param name="updateContactDto">Nouvelles données du contact</param>
    /// <param name="cancellationToken">Token d'annulation</param>
    /// <returns>Le contact mis à jour</returns>
    /// <response code="200">Contact mis à jour avec succès</response>
    /// <response code="400">Données invalides</response>
    /// <response code="404">Contact non trouvé</response>
    /// <response code="409">Email déjà utilisé</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ContactDto>> UpdateContact(
        int id,
        [FromBody] UpdateContactDto updateContactDto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedContact = await _contactService.UpdateContactAsync(id, updateContactDto, cancellationToken);

            if (updatedContact == null)
            {
                return NotFound($"Le contact avec l'ID {id} n'a pas été trouvé.");
            }

            return Ok(updatedContact);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Tentative de mise à jour avec un email existant pour le contact {ContactId}", id);
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la mise à jour du contact {ContactId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Une erreur interne s'est produite lors de la mise à jour du contact.");
        }
    }

    /// <summary>
    /// Supprime un contact
    /// </summary>
    /// <param name="id">Identifiant du contact à supprimer</param>
    /// <param name="cancellationToken">Token d'annulation</param>
    /// <returns>Confirmation de suppression</returns>
    /// <response code="204">Contact supprimé avec succès</response>
    /// <response code="404">Contact non trouvé</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteContact(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var deleted = await _contactService.DeleteContactAsync(id, cancellationToken);

            if (!deleted)
            {
                return NotFound($"Le contact avec l'ID {id} n'a pas été trouvé.");
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erreur lors de la suppression du contact {ContactId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Une erreur interne s'est produite lors de la suppression du contact.");
        }
    }
}