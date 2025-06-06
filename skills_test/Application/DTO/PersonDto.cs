using skills_test.Domain.Models;

namespace skills_test.Application.DTO;

public class PersonDto(long id, string name, string displayName, Skill[] skill)
{
    public long Id { get; set; }
    public string Name { get; set; } = name;
    public string DisplayName { get; set; } = displayName;
    public Skill[] Skill { get; set; } = skill;
}