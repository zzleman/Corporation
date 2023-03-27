using Corporation.Core.Interfaces;

namespace Corporation.Core.Entities;

public class Company : IEntity
{
    public int Id { get; private set; }

    public string Name { get; set; }

    private static int _count;

    public Company()
    {
        Id = _count++;
    }

    public Company(string name):this()
    {
        Name = name;
    }

   
}
