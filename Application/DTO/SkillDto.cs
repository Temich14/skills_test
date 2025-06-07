using System.ComponentModel.DataAnnotations;

namespace skills_test.Application.DTO;

public class SkillDto(string name, byte level)
{
    [Required(ErrorMessage = "Необходимо название навыка")]
    [MinLength(1)]
    public string Name { get; set; } = name;

    [Required(ErrorMessage = "Необходим уровень навыка")]
    [Range(1, 10, ErrorMessage = "Уровень должен быть в диапозоне от 1 до 10 включительно")]
    public byte Level { get; set; } = level;
}