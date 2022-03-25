using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using HallOfFame.BusinessLogic.Persons.Commands.CreatePerson;
using HallOfFame.Domain.Entities;
using HallOfFame.WebApi.Dto;
using HallOfFame.WebApi.ViewModels;
using Xunit;

namespace HallOfFame.IntegrationTests;

public class PersonControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public PersonControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
        _client = factory.CreateClient(/*new WebApplicationFactoryClientOptions { AllowAutoRedirect = false }*/);
    }

    [Fact]
    public async Task GetAllPersons_WhenCalled_ReturnsNonEmptySet()
    {
        var response = await _client.GetAsync("/api/v1/persons");

        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        string content = await response.Content.ReadAsStringAsync();
        var persons = JsonSerializer.Deserialize<List<Person>>(content, _options);
        persons!.Count.Should().BePositive();
    }

    [Fact]
    public async Task GetPersonById_WhenCalledWithExistingId_ReturnsPerson()
    {
        var response = await _client.GetAsync("/api/v1/person/2");

        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        string content = await response.Content.ReadAsStringAsync();
        var person = JsonSerializer.Deserialize<Person>(content, _options);
        person.Should().NotBeNull();
        person!.Name.Should().NotBeNull();
    }

    [Fact]
    public async Task GetPersonById_WhenCalledWithNonExistentId_Returns404ErrorCode()
    {
        var response = await _client.GetAsync("/api/v1/person/5");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        string content = await response.Content.ReadAsStringAsync();
        var errorDetails = JsonSerializer.Deserialize<ErrorDetails>(content, _options);
        errorDetails.Should().NotBeNull();
        errorDetails!.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task CreatePerson_WhenCalledWithValidData_ReturnsNewlyCreatedPersonId()
    {
        var skills = new List<Skill>
        {
            new() { Name = "OOP", Level = 5 },
            new() { Name = "SOLID", Level = 6 }
        };
        var command = new CreatePersonCommand("John Doe", "J_Doe", skills);
        string content = JsonSerializer.Serialize(command);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/v1/person", bodyContent);

        response.EnsureSuccessStatusCode();
        string stringId = await response.Content.ReadAsStringAsync();
        var id = JsonSerializer.Deserialize<long>(stringId, _options);
        id.Should().BePositive();
    }

    [Fact]
    public async Task CreatePerson_WhenCalledWithInvalidData_Returns400ErrorCode()
    {
        var skills = new List<Skill>
        {
            new() { Name = "OOP", Level = 15 },
            new() { Name = "SOLID", Level = 6 }
        };
        var command = new CreatePersonCommand("John Doe", "J_Doe", skills);
        string content = JsonSerializer.Serialize(command);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/api/v1/person", bodyContent);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        string responseContent = await response.Content.ReadAsStringAsync();
        var errorDetails = JsonSerializer.Deserialize<ErrorDetails>(responseContent, _options);
        errorDetails.Should().NotBeNull();
        errorDetails!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task UpdatePerson_WhenCalledWithValidData_Returns204StatusCode()
    {
        var skills = new List<Skill>
        {
            new() { Name = "OOP", Level = 5 },
            new() { Name = "SOLID", Level = 6 }
        };
        var person = new PersonDto { Name = "John Doe", DisplayName = "J_Doe", Skills = skills};
        string content = JsonSerializer.Serialize(person);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("/api/v1/person/1", bodyContent);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task UpdatePerson_WhenCalledWithInvalidData_Returns400ErrorCode()
    {
        var skills = new List<Skill>
        {
            new() { Name = "OOP", Level = 5 },
            new() { Name = "SOLID", Level = 6 }
        };
        var person = new PersonDto { Name = null, DisplayName = "J_Doe", Skills = skills};
        string content = JsonSerializer.Serialize(person);
        var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("/api/v1/person/1", bodyContent);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        string responseContent = await response.Content.ReadAsStringAsync();
        var errorDetails = JsonSerializer.Deserialize<ErrorDetails>(responseContent, _options);
        errorDetails.Should().NotBeNull();
        errorDetails!.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task DeletePerson_WhenCalledWithExistingId_Returns204StatusCode()
    {
        var response = await _client.DeleteAsync("/api/v1/person/3");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeletePerson_WhenCalledWithNonExistentId_Returns404ErrorCode()
    {
        var response = await _client.DeleteAsync("/api/v1/person/10");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        string content = await response.Content.ReadAsStringAsync();
        var errorDetails = JsonSerializer.Deserialize<ErrorDetails>(content, _options);
        errorDetails.Should().NotBeNull();
        errorDetails!.StatusCode.Should().Be(404);
    }
}