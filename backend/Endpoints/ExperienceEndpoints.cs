﻿using backend.Data.Mappers;
using backend.Services;

namespace backend.Endpoints;

public static class ExperienceEndpoints
{
    public static void MapExperienceEndpoints(this WebApplication app)
    {
        // GET /experiences
        app.MapGet(
                "/experiences",
                async (ICvService cvService) =>
                {
                    var experiences = await cvService.GetAllExperiencesAsync();
                    var experienceDtos = experiences.Select(u => u.ToDto()).ToList();

                    return Results.Ok(experienceDtos);
                }
            )
            .WithName("GetAllExperiences")
            .WithTags("Experiences");

        // GET /experiences/{id}
        app.MapGet(
                "/experiences/{id:guid}",
                async (Guid id, ICvService cvService) =>
                {
                    //Oppgave 2
                    var experience = await cvService.GetExperienceByIdAsync(id);
                    if (experience == null)
                    {
                        return Results.NotFound("The experience does not exist");
                    }
                    var experienceDto = experience.ToDto();

                    return Results.Ok(experienceDto);
                }
            )
            .WithName("GetExperienceById")
            .WithTags("Experiences");

        // GET /experiences/type/{type}
        app.MapGet(
                "/experiences/type/{type}",
                async (string type, ICvService cvService) =>
                {
                    // Oppgave 3
                    var experiencesByType = await cvService.GetExperiencesByTypeAsync(type);
                    if (!experiencesByType.Any())
                    {
                        return Results.NotFound("There exists no experiences of this type");
                    }

                    var experiencesByTypeDtos = experiencesByType.Select(u => u.ToDto()).ToList();

                    return Results.Ok(experiencesByTypeDtos);
                }
            )
            .WithName("GetExperiencesByType")
            .WithTags("Experiences");
    }
}
