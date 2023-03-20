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

    public Person Get(int id)
    {
        var person = _personsRepository.Get(id);
        if (person is null)
        {
            throw new PersonNotFoundException(id);
        }
        
        return person;
    }
}