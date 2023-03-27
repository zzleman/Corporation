using Corporation.Core.Entities;
using Corporation.Infrastructure.Utilities.Exceptions;
using Corporation.Infrastructure.DBContext;
namespace Corporation.Infrastructure.Services;

public class CompanyServices
{
	private static int counter = 0;
	public void Create(string? name)
	{
		if (String.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentNullException();
		}
		bool isExist = false;
		for (int i = 0; i < counter; i++)
		{
			if (AppDbContext.Companies[i].Name.ToUpper()==name.ToUpper() )
			{
				isExist = true; break;
			}
		}
		if (isExist)
		{
			throw new DuplicateNameException("This company name is already exist");
		}
		Company new_company = new (name);
		AppDbContext.Companies[counter++] = new_company;
	}

    public void GetAll()
    {
        Console.WriteLine("\n\tID \t\t Name");
        for (int i = 0; i < counter; i++)

        Console.WriteLine($"\n\t{AppDbContext.Companies[i].Id} \t\t {AppDbContext.Companies[i].Name}");

    }

 
}

