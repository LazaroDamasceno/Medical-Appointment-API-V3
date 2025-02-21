using v3.People.Domain;
using v3.People.DTOs;

namespace v3.People.Utils;

public class PersonResponseMapper
{
    public static PersonResponseDto Map(Person person)
    {
        return new PersonResponseDto(person.Id.ToString(), person.FullName());
    }
}