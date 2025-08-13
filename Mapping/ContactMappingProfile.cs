using AutoMapper;
using ContactManagement.Api.Models;
using ContactManagement.Api.Models.DTOs;

namespace ContactManagement.Api.Mapping;

/// <summary>
/// Profil de mapping AutoMapper pour les contacts
/// </summary>
public class ContactMappingProfile : Profile
{
    public ContactMappingProfile()
    {
        // Mapping Contact -> ContactDto
        CreateMap<Contact, ContactDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));

        // Mapping CreateContactDto -> Contact
        CreateMap<CreateContactDto, Contact>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // Mapping UpdateContactDto -> Contact
        CreateMap<UpdateContactDto, Contact>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
    }
}

