using WingsOn.Domain.Entities;

namespace WingsOn.Domain.Interfaces;

public interface IPersonService
{
    public Person Get(int personId);

    public void UpdateAddress(int personId, string address);
}