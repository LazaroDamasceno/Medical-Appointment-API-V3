using v3.People.Domain;
using v3.People.DTOs;

namespace v3.People.Services;

public interface IPersonModificationService
{
    Task<Person> ModifyAsync(Person person, PersonModificationDto modificationDto);
}