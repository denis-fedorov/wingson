using System;
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

    public void UpdateAddress(int personId, string address)
    {
        ArgumentException.ThrowIfNullOrEmpty(address);
        
        var person = Get(personId);
        person.Address = address;
        
        _personsRepository.Save(person);
    }
}