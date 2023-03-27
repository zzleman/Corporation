using Corporation.Core.Interfaces;

namespace Corporation.Core.Entities;

public class Employee : IEntity
{
	public int Id { get; private set; }

	public string Name { get; set; }

    public string Surname { get; set; }

    public double Salary { get; set; }

    public int DepartamentId { get; set; }

    private static int _count;

    public Employee()
    {
        Id = _count++;
    }

    public Employee(string name, string surname, double salary,int departament_Id):this()
    {
        Name = name;
        Surname = surname;
        Salary = salary;
        DepartamentId = departament_Id;
    }

    public override string ToString()
    {
       
       return $"{Id} {Name} {Surname} {Salary} ";
        
    }

}

