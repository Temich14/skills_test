namespace skills_test.Domain.Models;

public class Skill(string name, byte level)
{
    public string Name { get; set; } = name;
    public byte Level { get; set; } = level;
}