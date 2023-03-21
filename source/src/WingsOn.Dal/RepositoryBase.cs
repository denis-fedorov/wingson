using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WingsOn.Domain;
using WingsOn.Domain.Interfaces;

namespace WingsOn.Dal;

public class RepositoryBase<T> : IRepository<T> where T : DomainObject
{
    protected readonly CultureInfo DefaultCultureInfo = new("nl-NL");
    
    protected RepositoryBase()
    {
        Repository = new List<T>();
    }

    protected readonly List<T> Repository;

    public IEnumerable<T> GetAll()
    {
        return Repository;
    }

    public T Get(int id)
    {
        return GetAll().SingleOrDefault(a => a.Id == id);
    }

    public void Save(T element)
    {
        if (element == null)
        {
            return;
        }

        var existing = Get(element.Id);
        if (existing != null)
        {
            Repository.Remove(existing);
        }

        Repository.Add(element);
    }
}