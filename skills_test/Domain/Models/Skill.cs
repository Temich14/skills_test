namespace skills_test.Domain.Models;

public class Skill
{
    public Skill()
    {
    }

    public Skill(long id, string name, byte level)
    {
        Id = id;
        Name = name;
        Level = level;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public byte Level { get; set; }
}