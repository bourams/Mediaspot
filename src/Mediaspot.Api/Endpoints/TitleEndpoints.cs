using Mediaspot.Api.DTOs.Titles;
using Mediaspot.Application.Titles.Commands;
using Mediaspot.Application.Titles.Queries.GetById;
using MediatR;

namespace Mediaspot.Api.Endpoints;

public static class TitleEndpoints
{
    public static void MapTitleEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/titles")
                       .WithTags("Titles");

        group.MapPost("", async (CreateTitleDto dto, ISender sender) =>
        {
            // Auto mapper ?
            var cmd = new CreateTitleCommand(dto.Name, dto.Description, dto.ReleaseDate, dto.Type);
            var id = await sender.Send(cmd);
            var response = new CreateTitleResponseDto(id.ToString());

            return TypedResults.CreatedAtRoute(response, "GetTitleById", new { id });
        })
        .WithName("PostCreateTitle")
        .Produces<CreateTitleResponseDto>(StatusCodes.Status201Created)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest);
        // TODO: StatusCodes.Status409Conflict
        // TODO: StatusCodes.Status500InternalServerError

        group.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
        {
            var response = await sender.Send(new GetTitleByIdQuery(id));
            var title = new TitleDto(response.Name, response.Description, response.ReleaseDate, response.Type);

            return TypedResults.Ok(title);
        })
        .WithName("GetTitleById")
        .WithOpenApi()
        .Produces<TitleDto>(StatusCodes.Status200OK);
        // TODO: StatusCodes.Status404NotFound
        // TODO: StatusCodes.Status500InternalServerError
    }
}
