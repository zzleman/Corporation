using Corporation.Core.Interfaces;

namespace Corporation.Core.Entities;

public class Departament : IEntity
{
    public int Id { get; private set; }

    public string Name { get; set; }

    public int EmployeeLimit { get; set; }

    public int CountNow { get; set; }

    public int CompanyId { get; set; }

    private static int _count;

    public Departament()
    {
        Id = _count++;
    }

    public Departament(string name, int employeelimit,int company_Id):this()
    {
        Name = name;
        EmployeeLimit = employeelimit;
        CompanyId = company_Id;
        CountNow=0;
     
    }
    public void AddEmployee(Employee employee)
    {
        CountNow++;
        employee.DepartamentId =this.Id;
    }

    public override string ToString()
    {
        return $"{Id} {Name}";
    }

   

}

