using AutoMapper;
using ContactManagement.Api.Data;
using ContactManagement.Api.Models;
using ContactManagement.Api.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ContactManagement.Api.Services;

/// <summary>
/// Service de gestion des contacts
/// </summary>
public class ContactService : IContactService
{
    private readonly ContactManagementDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<ContactService> _logger;
    public ContactService(
        ContactManagementDbContext context,
        IMapper mapper,
        ILogger<ContactService> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<PagedResultDto<ContactDto>> GetContactsAsync(ContactSearchDto searchDto, CancellationToken cancellationToken = default)
    {
        var query = _context.Contacts.AsQueryable();

        // Appliquer le filtre de recherche
        if (!string.IsNullOrWhiteSpace(searchDto.SearchTerm))
        {
            var searchTerm = searchDto.SearchTerm.ToLower();
            query = query.Where(c =>
                c.FirstName.ToLower().Contains(searchTerm) ||
                c.LastName.ToLower().Contains(searchTerm) ||
                c.Email.ToLower().Contains(searchTerm));
        }

        // Appliquer le tri
        query = searchDto.SortBy.ToLower() switch
        {
            "firstname" => searchDto.SortOrder.ToLower() == "desc"
                ? query.OrderByDescending(c => c.FirstName)
                : query.OrderBy(c => c.FirstName),
            "email" => searchDto.SortOrder.ToLower() == "desc"
                ? query.OrderByDescending(c => c.Email)
                : query.OrderBy(c => c.Email),
            "createdat" => searchDto.SortOrder.ToLower() == "desc"
                ? query.OrderByDescending(c => c.CreatedAt)
                : query.OrderBy(c => c.CreatedAt),
            _ => searchDto.SortOrder.ToLower() == "desc"
                ? query.OrderByDescending(c => c.LastName)
                : query.OrderBy(c => c.LastName)
        };

        // Calculer le nombre total d'éléments
        var totalCount = await query.CountAsync(cancellationToken);

        // Appliquer la pagination
        var pageSize = Math.Min(searchDto.PageSize, 100); // Limiter à 100 éléments max
        var skip = (searchDto.PageNumber - 1) * pageSize;

        var contacts = await query
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var contactDtos = _mapper.Map<IEnumerable<ContactDto>>(contacts);

        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        _logger.LogInformation("Retrieved {Count} contacts (page {PageNumber}/{TotalPages})",
            contacts.Count, searchDto.PageNumber, totalPages);

        return new PagedResultDto<ContactDto>
        {
            Items = contactDtos,
            PageNumber = searchDto.PageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }

    public async Task<ContactDto?> GetContactByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (contact == null)
        {
            _logger.LogWarning("Contact with ID {ContactId} not found", id);
            return null;
        }

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task<ContactDto> CreateContactAsync(CreateContactDto createContactDto, CancellationToken cancellationToken = default)
    {
        // Vérifier si l'email existe déjà
        if (await IsEmailExistsAsync(createContactDto.Email, cancellationToken: cancellationToken))
        {
            _logger.LogWarning("Attempt to create contact with existing email: {Email}", createContactDto.Email);
            throw new InvalidOperationException($"Un contact avec l'email '{createContactDto.Email}' existe déjà.");
        }

        var contact = _mapper.Map<Contact>(createContactDto);
        contact.CreatedAt = DateTime.UtcNow;
        contact.UpdatedAt = DateTime.UtcNow;

        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync(cancellationToken);



        _logger.LogInformation("Created new contact with ID {ContactId} and email {Email}",
            contact.Id, contact.Email);

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task<ContactDto?> UpdateContactAsync(int id, UpdateContactDto updateContactDto, CancellationToken cancellationToken = default)
    {
        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (contact == null)
        {
            _logger.LogWarning("Attempt to update non-existent contact with ID {ContactId}", id);
            return null;
        }

        // Vérifier si l'email existe déjà (en excluant le contact actuel)
        if (await IsEmailExistsAsync(updateContactDto.Email, id, cancellationToken))
        {
            _logger.LogWarning("Attempt to update contact {ContactId} with existing email: {Email}", id, updateContactDto.Email);
            throw new InvalidOperationException($"Un contact avec l'email '{updateContactDto.Email}' existe déjà.");
        }

        // Mettre à jour les propriétés
        _mapper.Map(updateContactDto, contact);
        contact.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated contact with ID {ContactId}", id);

        return _mapper.Map<ContactDto>(contact);
    }

    public async Task<bool> DeleteContactAsync(int id, CancellationToken cancellationToken = default)
    {
        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (contact == null)
        {
            _logger.LogWarning("Attempt to delete non-existent contact with ID {ContactId}", id);
            return false;
        }

        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted contact with ID {ContactId} and email {Email}",
            id, contact.Email);

        return true;
    }

    public async Task<bool> IsEmailExistsAsync(string email, int? excludeContactId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Contacts.Where(c => c.Email.ToLower() == email.ToLower());

        if (excludeContactId.HasValue)
        {
            query = query.Where(c => c.Id != excludeContactId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }


}

