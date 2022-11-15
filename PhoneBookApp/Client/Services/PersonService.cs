using Microsoft.AspNetCore.Components;
using PhoneBookApp.Shared;
using System;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace PhoneBookApp.Client.Services
{
    public class PersonService : IPersonService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;
        public PersonService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public List<Person> People { get; set; } = new List<Person>();

        public async Task AddPerson(Person person)
        {
            var result = await _http.PostAsJsonAsync("api/person", person);
            await SetPeople(result);
        }

        private async Task SetPeople(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Person>>();
            People = response;
            _navigationManager.NavigateTo("people");
        }

        public async Task DeletePerson(int id)
        {
            var result = await _http.DeleteAsync($"api/person/{id}");
            await SetPeople(result);
        }

        public async Task GetPeople()
        {
            var result = await _http.GetFromJsonAsync<List<Person>>("api/person");

            if(result != null)
                People = result;
        }

        public async Task<Person> GetPersonById(int id)
        {
            var result = await _http.GetFromJsonAsync<Person>($"api/person/{id}");

            if (result != null)
                return result;
            throw new Exception("Person not found!");
        }

        public async Task UpdatePerson(Person person)
        {
            var result = await _http.PutAsJsonAsync($"api/person/{person.Id}", person);
            await SetPeople(result);
        }
    }
}
