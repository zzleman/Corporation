using Corporation.Core.Entities;
using Corporation.Infrastructure.DBContext;
using Corporation.Infrastructure.Services;
using Corporation.Infrastructure.Utilities.Exceptions;
using Corporation.Infrastructure.Utilities.Helper;
CompanyServices companyServices = new CompanyServices();
DepartamentServices departamentServices = new DepartamentServices();
EmployeeServices employeeServices = new EmployeeServices();
Options options = new Options();

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("Welcome!");
while (true)
{
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine($"\nPress 0 to exit" +
        $"\nPress 1 : to Create Company" +
        $"\nPress 2 : to List Companies" +
        $"\nPress 3 : to Create Departament" +
        $"\nPress 4 : to List Departaments" +
        $"\nPress 5 : to Get All Departaments by Company" +
        $"\nPress 6 : to Update Departament" +
        $"\nPress 7 : to Add Employee" +
        $"\nPress 8 : to List Employees" +
        $"\nPress 9 : to Get All Employees By Departament\n");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Please, enter your choice:");
    string? response = Console.ReadLine();
    int menu;
    bool TryToInt = int.TryParse(response, out menu);
    if (TryToInt)
    {
        switch (menu)
        {
            case (int)Options.Exit:
                Environment.Exit(0);
                break;
            case (int)Options.CreateCompany:
                Console.Clear();
                Console.WriteLine("Enter Company Name:");
                try
                {
                    string? response_coursename = Console.ReadLine();
                    companyServices.Create(response_coursename);
                    Console.WriteLine($"New Company : {response_coursename}");
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.ListCompanies:
                Console.WriteLine("\t------Companies' List------");
                companyServices.GetAll();
                break;
            case (int)Options.CreateDepartament:
               select_dept_name:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter Departament Name:");
                string? departament_name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(departament_name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Value cannot be null.\n");
                    goto select_dept_name;
                }
            employee_limit:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter Employee Limit:");
                string? departament_limit = Console.ReadLine();
                int employee_limit;
                bool TryToLimit = int.TryParse(departament_limit, out employee_limit);
                if (!TryToLimit)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The value cannot be null.\n");
                    goto employee_limit;
                }
                if (employee_limit == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The value cannot be zero");
                    goto employee_limit;
                }
                if (employee_limit < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The value cannot be negative");
                    goto employee_limit;
                }
            select_company:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter Company (Id):");
                companyServices.GetAll();
                string? select_company = Console.ReadLine();
                int company_Id;
                bool tryToIdCompany = int.TryParse(select_company, out company_Id);
                if (!tryToIdCompany)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter correct Company Id\n");
                    goto select_company;
                }
                try
                {
                    departamentServices.Create(departament_name, employee_limit, company_Id);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Succesfully Created!");
                }
                catch (AddCompanyFailedException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    goto select_company;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    goto case Options.CreateDepartament;
                }
                break;
            case (int)Options.ListDepartaments:
                Console.WriteLine("\t\t\t-------Departaments' List-------");
                departamentServices.GetAll();
                break;
            case (int)Options.GetAllDepartaments:
                
                Console.WriteLine("Enter Company Name:");
                string? searchname = Console.ReadLine();
                try
                {
                    departamentServices.GetDepartamentByName(searchname);
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (NotExistException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.UpdateDepartaments:
                departamentServices.GetAll();
            select_update:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Select To Update:");
                string? update = Console.ReadLine();
                int updateId;
                bool TryToUpdateId = int.TryParse(update, out updateId);
                if (string.IsNullOrWhiteSpace(update))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Value cannot be null.\n");
                    goto select_update;
                }
                if (!TryToUpdateId)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter correct type");
                    goto select_update;
                }

            option_name:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter Departament Name:");
                string? name_updated = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name_updated))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Value cannot be null.\n");
                    goto option_name;
                }
            new_limit:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter Employee Limit:");
                string? employeeLimit = Console.ReadLine();
                int limit;
                bool TryToEmpLimit = int.TryParse(employeeLimit, out limit);
                if (!TryToEmpLimit)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter Correct Option");
                }
                if (limit == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The value cannot be zero");
                    goto new_limit;
                }
                if (limit < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The value cannot be negative");
                    goto new_limit;
                }
                try
                {
                    departamentServices.UpdateDepartaments(updateId, name_updated, limit);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Updated Succesfully!");
                }
                catch (NotExistException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    goto select_update;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    goto case Options.UpdateDepartaments;
                }

                break;
            case (int)Options.AddEmployee:
                select_name:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter Employee Name:");
                string? employee_name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(employee_name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Value cannot be null.");
                    goto select_name;
                }
                select_surname:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter Employee Surname:");
                string? employee_surname = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(employee_surname))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Value cannot be null.");
                    goto select_surname;

                }
            employee_salary:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter Employee Salary:");
                string? salary = Console.ReadLine();
                double employee_salary;
                bool TryToSalary = double.TryParse(salary,out employee_salary);
                if (!TryToSalary)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter correct format:");
                    goto employee_salary;
                }
                if(employee_salary == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Salary cannot be zero");
                    goto employee_salary;
                }
                if(employee_salary < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Salary cannot be negative");
                    goto employee_salary;
                }
            select_departament:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter Departament (Id):");
                departamentServices.GetAll();
                string? select_departament = Console.ReadLine();
                int departament_Id;
                bool tryToIdDepartament = int.TryParse(select_departament, out departament_Id);
                if (!tryToIdDepartament)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter correct Group Id");
                    goto select_departament;
                }
                try
                {
                    employeeServices.Create(employee_name, employee_surname, employee_salary, departament_Id);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Succesfully added");

                }
                catch (AddDepartamentFailedException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    goto select_departament;
                }
                catch (CapacityLimitException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);

                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine(ex.Message);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    goto case Options.AddEmployee;
                }
                
                break;
            case (int)Options.ListEmployees:
                Console.WriteLine("\t------Employees' List------");
                employeeServices.GetAll();
                break;
            case (int)Options.GetAllEmployees:
               choose_getemployee:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Enter Departament Name:");
                string? nameoflist = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nameoflist))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Value cannot be null.");
                    goto choose_getemployee;
                }

                try
                {
                    employeeServices.GetEmployeeByName(nameoflist);
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (NotExistException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
                break;
            default:
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Choose valid menu option!");
                break;
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Please, enter correct menu item");
    }
    
}
