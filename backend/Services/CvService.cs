﻿using backend.Data;
using backend.Data.Mappers;
using backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class CvService(AppDbContext context) : ICvService
{
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await context.Users.OrderBy(u => u.Name).ToListAsync();
    }

    // Oppgave 1
    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<IEnumerable<Experience>> GetAllExperiencesAsync()
    {
        // Oppgave 2
        return await context.Experiences.OrderBy(u => u.Title).ToListAsync();
    }

    public async Task<Experience?> GetExperienceByIdAsync(Guid id)
    {
        // Oppgave 2
        return await context.Experiences.FindAsync(id);
    }

    public async Task<IEnumerable<Experience?>> GetExperiencesByTypeAsync(string type)
    {
        // Oppgave 3
        return await context.Experiences.Where(experience => experience.Type == type).ToListAsync();
    }

    // TODO: Oppgave 4 ny metode (husk å legge den til i interfacet)
}
