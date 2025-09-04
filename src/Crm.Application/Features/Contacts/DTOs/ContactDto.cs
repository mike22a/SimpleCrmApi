using Crm.Domain.Models;

namespace Crm.Application.Features.Contacts.DTOs;

public class ContactDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }

    // create mapping from Contact
    public static ContactDto From(Contact contact)
    {
        var dto = new ContactDto();
        dto.Id = contact.Id;
        dto.FirstName = contact.FirstName;
        dto.LastName = contact.LastName;
        dto.Email = contact.Email;
        dto.PhoneNumber = contact.PhoneNumber;
        return dto;
    }

    // create mapping from Contacts
    public static IEnumerable<ContactDto> From(IEnumerable<Contact> contacts)
    {
        return contacts.Select(c => ContactDto.From(c));
    }
}