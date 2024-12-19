using ConsoleEmployeeAppCRUD.Service;
using ConsoleEmployeeAppCRUD.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleEmployeeAppCRUD.Repository;
using layeredarchitecturedemo.Repository;
using layeredarchitecturedemo.Service;

namespace ConsoleEmployeeAppCRUD
{
    internal class EmployeeApp
    {
        static async Task Main(string[] args)
        {
            IEmployeeService employeeService = new EmployeeServiceImpl(new EmployeeRepositoryImpl());

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nEmployee Management System");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Update Employee");
                Console.WriteLine("3. Get Employee by Code");
                Console.WriteLine("4. Get All Employees");
                Console.WriteLine("5. Delete Employee");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddEmployee(employeeService);
                        break;
                    case "2":
                        await UpdateEmployee(employeeService);
                        break;
                    case "3":
                        await SearchEmployee(employeeService);
                        break;
                    case "4":
                        await ListEmployees(employeeService);
                        break;
                    case "5":
                        await DeleteEmployee(employeeService);
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        // Add Employee
        private static async Task AddEmployee(IEmployeeService employeeService)
        {
            var employee = new Employee();

            Console.Write("Enter Employee Code: ");
            employee.EmployeeCode = Console.ReadLine();

            Console.Write("Enter Employee Name: ");
            employee.EmployeeName = Console.ReadLine();

            Console.Write("Enter Department Code: ");
            employee.DepartmentCode = Console.ReadLine();

            Console.Write("Enter Location Code: ");
            employee.LocationCode = Console.ReadLine();

            Console.Write("Enter Salary: ");
            employee.Salary = int.Parse(Console.ReadLine());

            await employeeService.AddEmployeeAsync(employee);
            Console.WriteLine("Employee added successfully.");
        }

        // Update Employee
        private static async Task UpdateEmployee(IEmployeeService employeeService)
        {
            Console.Write("Enter Employee Code to update: ");
            string code = Console.ReadLine();

            // Fetch the existing employee details to show current values
            var employee = await employeeService.GetEmployeeByCodeAsync(code);

            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            var updatedEmployee = new Employee();

            Console.WriteLine($"Current Name: {employee.EmployeeName}");
            Console.Write("Enter Updated Employee Name (press Enter to keep current): ");
            string employeeName = Console.ReadLine();
            updatedEmployee.EmployeeName = string.IsNullOrWhiteSpace(employeeName) ? employee.EmployeeName : employeeName;

            Console.WriteLine($"Current Department Code: {employee.DepartmentCode}");
            Console.Write("Enter Updated Department Code (press Enter to keep current): ");
            string departmentCode = Console.ReadLine();
            updatedEmployee.DepartmentCode = string.IsNullOrWhiteSpace(departmentCode) ? employee.DepartmentCode : departmentCode;

            Console.WriteLine($"Current Location Code: {employee.LocationCode}");
            Console.Write("Enter Updated Location Code (press Enter to keep current): ");
            string locationCode = Console.ReadLine();
            updatedEmployee.LocationCode = string.IsNullOrWhiteSpace(locationCode) ? employee.LocationCode : locationCode;

            // Handle salary update (ensure it is a valid number)
            Console.WriteLine($"Current Salary: {employee.Salary}");
            Console.Write("Enter Updated Salary (press Enter to keep current): ");
            string salaryInput = Console.ReadLine();
            updatedEmployee.Salary = string.IsNullOrWhiteSpace(salaryInput) ? employee.Salary : int.TryParse(salaryInput, out var salary) ? salary : employee.Salary;

            await employeeService.UpdateEmployeeAsync(code, updatedEmployee);
            Console.WriteLine("Employee updated successfully.");
        }


        // Search Employee by Code
        private static async Task SearchEmployee(IEmployeeService employeeService)
        {
            Console.Write("Enter Employee Code to search: ");
            string code = Console.ReadLine();

            var employee = await employeeService.GetEmployeeByCodeAsync(code);
            if (employee != null)
            {
                Console.WriteLine($"Code: {employee.EmployeeCode}, Name: {employee.EmployeeName}, " +
                                  $"Department: {employee.DepartmentCode}, Location: {employee.LocationCode}, " +
                                  $"Salary: {employee.Salary}");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }

        // List All Employees
        private static async Task ListEmployees(IEmployeeService employeeService)
        {
            var employees = await employeeService.GetAllEmployeesAsync();
            foreach (var emp in employees)
            {
                Console.WriteLine($"Code: {emp.EmployeeCode}, Name: {emp.EmployeeName}, " +
                                  $"Department: {emp.DepartmentCode}, Location: {emp.LocationCode}, " +
                                  $"Salary: {emp.Salary}");
            }
        }

        // Delete Employee
        private static async Task DeleteEmployee(IEmployeeService employeeService)
        {
            Console.Write("Enter Employee Code to delete: ");
            string code = Console.ReadLine();

            await employeeService.DeleteEmployeeAsync(code);
            Console.WriteLine("Employee deleted successfully.");
        }
    }
}
