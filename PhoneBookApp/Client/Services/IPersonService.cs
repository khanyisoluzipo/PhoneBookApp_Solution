using PhoneBookApp.Shared;

namespace PhoneBookApp.Client.Services
{
    public interface IPersonService
    {
        List<Person> People { get; set; }
        Task GetPeople();
        Task<Person> GetPersonById(int id);
        Task AddPerson(Person person);
        Task UpdatePerson(Person person);
        Task DeletePerson(int id);
    }
}
