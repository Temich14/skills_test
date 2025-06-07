using Microsoft.EntityFrameworkCore;
using skills_test.Core;
using skills_test.Domain.Models;
using skills_test.Domain.Ports;

namespace skills_test.Infrastructure.Data;

public sealed class PersonRepository(AppDbContext context) : IPersonRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Person> CreatePerson(Person person)
    {
        await _context.Persons.AddAsync(person);

        await _context.SaveChangesAsync();

        return person;
    }

    public async Task<Person?> UpdatePerson(Person person)
    {
        var personToUpdate = await _context.Persons
            .Include(p => p.Skill)
            .FirstOrDefaultAsync(p => p.Id == person.Id);
        if (personToUpdate == null)
        {
            return null;
        }

        personToUpdate.Name = person.Name;
        personToUpdate.DisplayName = person.DisplayName;
        personToUpdate.Skill.Clear();
        personToUpdate.Skill = person.Skill;

        await _context.SaveChangesAsync();
        return personToUpdate;
    }

    public async Task<bool> DeletePerson(long id)
    {
        var personToDelete = await _context.Persons.FindAsync(id);
        if (personToDelete == null)
        {
            return false;
        }

        _context.Persons.Remove(personToDelete);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Person?> GetPerson(long id)
    {
        return await _context.Persons
            .Include(p => p.Skill)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Person>> GetAllPersons()
    {
        return await _context.Persons.Include(p => p.Skill).ToListAsync();
    }
}