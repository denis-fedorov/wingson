using WingsOn.Domain.Entities;

namespace WingsOn.Domain.Interfaces;

public interface IPersonService
{
    public Person Get(int id);
}