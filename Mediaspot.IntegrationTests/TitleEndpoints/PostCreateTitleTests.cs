using FluentAssertions;
using Mediaspot.Api.DTOs.Titles;
using Mediaspot.Api.Endpoints;
using Mediaspot.Domain.Titles;
using Mediaspot.IntegrationTests.Fixtures;
using System.Net;
using System.Net.Http.Json;

namespace Mediaspot.IntegrationTests.TitleEndpoints;

public sealed class PostCreateTitleTests : IClassFixture<InMemoryDbWebApplication>
{
    private readonly HttpClient _client;

    public PostCreateTitleTests(InMemoryDbWebApplication factory)
    {
        _client = factory.CreateClient();
    }

    //TODO: Ameliorer Assert
    [Fact]
    public async Task PostCreateTitle_Should_Return_201_When_Payload_Valid()
    {
        // Arrange
        var dto = new CreateTitleDto("Title", "Desc", new DateOnly(2025, 08, 21), TitleType.Movie);

        // Act
        var response = await _client.PostAsJsonAsync("titles", dto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();

        var idResp = await response.Content.ReadFromJsonAsync<CreateTitleResponseDto>();
        idResp.Should().NotBeNull();
        idResp!.Id.Should().NotBeEmpty();
    }
}
