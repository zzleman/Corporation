using Corporation.Core.Interfaces;

using Corporation.Core.Entities;

namespace Corporation.Infrastructure.DBContext;

public class AppDbContext
{
	public static Employee[] Employees { get; set; } = new Employee[2000];

	public static Departament[] Departaments { get; set; } = new Departament[100];

	public static Company[] Companies { get; set; } = new Company[100];

}




