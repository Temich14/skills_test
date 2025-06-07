namespace skills_test.Domain.Models;

public class Person
{
    public Person()
    {
    }

    public Person(long id, string name, string displayName, List<Skill> skill)
    {
        Id = id;
        Name = name;
        DisplayName = displayName;
        Skill = skill;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public List<Skill> Skill { get; set; }
}