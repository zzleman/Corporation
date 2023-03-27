using Corporation.Core.Entities;
using Corporation.Infrastructure.Utilities.Exceptions;
using Corporation.Infrastructure.DBContext;
namespace Corporation.Infrastructure.Services;

	public class DepartamentServices
	{
    private static int counter = 0;
    public void Create(string? dep_name,int employee_limit,int company_Id)
    {
        if (String.IsNullOrWhiteSpace(dep_name))
        {
            throw new ArgumentNullException();
        }
        bool isExist = false;
        for (int i = 0; i < counter; i++)
        {
            if (AppDbContext.Departaments[i].Name.ToUpper() == dep_name.ToUpper() && company_Id==AppDbContext.Departaments[i].CompanyId)
            {
                throw new DuplicateNameException("This departaIment name is already exist");

            }
        }
     

        foreach (var company in AppDbContext.Companies)
        {
            if (company is null)
            {
                throw new AddCompanyFailedException("This company is not exist");
            }

            if (company.Id == company_Id) { break; }
        }
        Departament new_departament = new(dep_name, employee_limit,company_Id);
        AppDbContext.Departaments[counter++] = new_departament;
    }

    public void GetAll()
    {
        Console.WriteLine("\n\tID \t Departament \t Employee Limit \t Belongs to");
        for (int i = 0; i < counter; i++)
        {
            String temp_company = String.Empty;
            foreach (var company in AppDbContext.Companies)
            {
                if (company == null) break;
                if (AppDbContext.Departaments[i].CompanyId == company.Id)
                {
                    temp_company = company.Name;
                    break;
                }
            }
            Console.WriteLine($"\n\t{AppDbContext.Departaments[i].Id} \t {AppDbContext.Departaments[i].Name} \t\t {AppDbContext.Departaments[i].EmployeeLimit} \t\t\t {temp_company}");
          
        }
    }

    public void GetById(int id)
    {
        for (int i = 0; i < counter; i++)
        {
            if (AppDbContext.Departaments[i].Id == id)
            {
                String company = String.Empty;
                foreach (var item in AppDbContext.Companies)
                {
                    if (item == null) continue;
                    if (item.Id == AppDbContext.Departaments[i].Id)
                    {
                        company = item.Name;
                        break;
                    }
                }
                Console.WriteLine($"\n\t{AppDbContext.Departaments[i].Id} \t {AppDbContext.Departaments[i].Name} \t\t {AppDbContext.Departaments[i].EmployeeLimit} \t\t\t {company}");
                return;
            }
        }
    }

    public void UpdateDepartaments(int update,string name, int employee_limit)
    {
        for (int i = 0; i < AppDbContext.Departaments.Length; i++)
        {
            if (AppDbContext.Departaments[i].Id == update)
            {
                AppDbContext.Departaments[update].Name = name;
                AppDbContext.Departaments[update].EmployeeLimit = employee_limit;
                break;
            }
            else
            {
                throw new NotExistException("Choose valid option.");
            }
        }

    }

    public void GetDepartamentByName(string searchname)
    {

        for (int i = 0; i < AppDbContext.Companies.Length; i++)
        {
            if (AppDbContext.Companies[i].Name.ToUpper().Equals(searchname.ToUpper()))
            {
                Console.WriteLine("Departaments:");
                foreach (var departament in AppDbContext.Departaments)
                {
                    if (departament == null) break;
                    if (departament.CompanyId == AppDbContext.Companies[i].Id)
                    {
                           Console.WriteLine(departament.Name);
                    }
                }
                break;

            }
            else
            {
                throw new NotExistException("There is not such named company");
            }
        }

    }


}

