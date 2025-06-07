using System.ComponentModel.DataAnnotations;
using skills_test.Domain.Models;

namespace skills_test.Application.DTO;

public class PersonResponseDto(long id, string name, string displayName, List<SkillDto> skill)
{
    public long Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string DisplayName { get; set; } = displayName;
    public List<SkillDto> Skill { get; set; } = skill;
}