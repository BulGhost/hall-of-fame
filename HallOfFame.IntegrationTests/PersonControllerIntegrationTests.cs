using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace HallOfFame.IntegrationTests;

public class PersonControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public PersonControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
    }

    [Fact]
    public async Task Post_DeleteAllMessagesHandler_ReturnsRedirectToRoot()
    {
        var response = await _client.GetAsync("/api/v1/persons");

        response.EnsureSuccessStatusCode();
        string responseString = await response.Content.ReadAsStringAsync();

        Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
    }
}