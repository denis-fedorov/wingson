using WingsOn.Domain.Entities;
using WingsOn.Domain.Exceptions;
using WingsOn.Domain.Interfaces;

namespace WingsOn.Domain.Services;

public sealed class PersonService : IPersonService
{
    private readonly IRepository<Person> _personsRepository;

    public PersonService(IRepository<Person> personsRepository)
    {
        _personsRepository = personsRepository;
    }

    public Person Get(int personId)
    {
        var person = _personsRepository.Get(personId);
        if (person is null)
        {
            throw new PersonNotFoundException(personId);
        }
        
        return person;
    }
}