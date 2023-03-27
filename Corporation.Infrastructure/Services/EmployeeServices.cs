using Corporation.Core.Entities;
using Corporation.Infrastructure.Utilities.Exceptions;
using Corporation.Infrastructure.DBContext;
using System;
using System.Xml.Linq;

namespace Corporation.Infrastructure.Services
{
    public class EmployeeServices
    {
        private static int counter = 0;
        private DepartamentServices departamentServices;
        public EmployeeServices()
        {
            departamentServices = new DepartamentServices();
        }

        public void Create(string? name, string surname, double salary, int departament_Id)
        {
            if (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(surname))
            {
                throw new ArgumentNullException();
            }
            Departament departament1 = null;
            foreach (var departament in AppDbContext.Departaments)
            {
                if (departament is null)
                {
                    throw new AddDepartamentFailedException("This departament is not exist");
                }
                if (departament.Id == departament_Id)
                {
                    departament1 = departament;
                    break;
 
                }
                
            }
            

            if (departament1.CountNow >= departament1.EmployeeLimit)
            {

                throw new CapacityLimitException("You have exceeded the employee limit.");
            }
            Employee new_employee = new(name, surname, salary, departament_Id);
            AppDbContext.Employees[counter++] = new_employee;
            departament1.AddEmployee(new_employee);
        }

        public void GetAll()
        {
            Console.WriteLine("\n\tID \t\tName\t   \t\tSurname\t   \t\tSalary\t   \t\tBelongs to\t ");
            for (int i = 0; i < counter; i++)
            {

                String temp_departament = String.Empty;
                foreach (var departament in AppDbContext.Departaments)
                {
                    if (departament == null) break;
 
                    if (AppDbContext.Employees[i].DepartamentId == departament.Id)
                    {
                        temp_departament = departament.Name;
                        break;
                    }
                }
                
                Console.WriteLine($"\n\t{AppDbContext.Employees[i].Id} \t\t{AppDbContext.Employees[i].Name} \t\t        {AppDbContext.Employees[i].Surname}\t\t {AppDbContext.Employees[i].Salary}    \t\t  {temp_departament} ");
                

            }

        }

        public void GetEmployeeByName(string nameoflist)
        {
            bool existDepartament = false;
            foreach (var departament in AppDbContext.Departaments)
            {
                if (departament.Name.ToUpper().Equals((nameoflist.ToUpper())))
                {
                    Console.WriteLine("Employees:");
                    foreach (var employee in AppDbContext.Employees)
                    {
                        if (employee != null && employee.DepartamentId==departament.Id)
                        {
                            Console.WriteLine(employee.Name+" "+employee.Surname);
                        }
                    }
                    existDepartament = true;
                    break;
                }
               
            }
            if (!existDepartament)
            {
                throw new NotExistException("There is not such named departament!");
            }
        }



    }
}
