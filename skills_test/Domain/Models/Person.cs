namespace skills_test.Domain.Models;

public class Person(long id, string name, string displayName, Skill[] skill)
{
    public long Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string DisplayName { get; set; } = displayName;
    public Skill[] Skill { get; set; } = skill;
}