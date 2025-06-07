using System.ComponentModel.DataAnnotations;
using skills_test.Domain.Models;

namespace skills_test.Application.DTO;

public class PersonRequestDto(string name, string displayName, List<SkillDto> skill)
{
    [MinLength(1, ErrorMessage = "Имя не должно быть пустым")]
    [Required(ErrorMessage = "Имя обязательно")]
    public string Name { get; set; } = name;

    [MinLength(1, ErrorMessage = "Отображаемое имя не должно быть пустым")]
    [Required(ErrorMessage = "Отображаемое имя обязательно")]
    public string DisplayName { get; set; } = displayName;

    public List<SkillDto> Skill { get; set; } = skill;
}