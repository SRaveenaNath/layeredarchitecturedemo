using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEmployeeAppCRUD.Model
{
    public class Employee
    {
        // Auto-generated field
        private int Id;


        // Properties
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentCode { get; set; }
        public string LocationCode { get; set; }
        public int Salary { get; set; }

        // Default constructor
        public Employee() { }

        // Parameterized constructor
        public Employee(string employeeCode, string employeeName, string departmentCode, string locationCode, int salary)
        {
            this.EmployeeCode = employeeCode;
            this.EmployeeName = employeeName;
            this.DepartmentCode = departmentCode;
            this.LocationCode = locationCode;
            this.Salary = salary;
        }
    }
}
