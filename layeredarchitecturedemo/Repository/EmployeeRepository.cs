using ConsoleEmployeeAppCRUD.Model;
using ConsoleEmployeeAppCRUD.Repository;
using layeredarchitecturedemo.Model;
using layeredarchitecturedemo.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace layeredarchitecturedemo.Repository
{
    public class EmployeeRepositoryImpl : IEmployeeRepository
    {
        private readonly string winConnString = ConfigurationManager.ConnectionStrings["CsWin"].ConnectionString;

        // Insert
        public async Task AddEmployeeAsync(Employee employee)
        {
            using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(winConnString))
            {
                string query = "INSERT INTO Employee (EmployeeCode, EmployeeName, DepartmentCode, LocationCode, Salary)" +
                               "VALUES(@EmpCode, @EmpName, @DeptCode, @LocCode, @Sal)";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@EmpCode", employee.EmployeeCode);
                    command.Parameters.AddWithValue("@EmpName", employee.EmployeeName);
                    command.Parameters.AddWithValue("@DeptCode", employee.DepartmentCode);
                    command.Parameters.AddWithValue("@LocCode", employee.LocationCode);
                    command.Parameters.AddWithValue("@Sal", employee.Salary);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        // Search By Employee Code
        public async Task<Employee> GetEmployeeByCodeAsync(string employeeCode)
        {
            using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(winConnString))
            {
                string query = "SELECT * FROM Employee WHERE EmployeeCode = @EmpCode";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@EmpCode", employeeCode);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Employee
                            {
                                EmployeeCode = reader["EmployeeCode"].ToString(),
                                EmployeeName = reader["EmployeeName"].ToString(),
                                DepartmentCode = reader["DepartmentCode"].ToString(),
                                LocationCode = reader["LocationCode"].ToString(),
                                Salary = Convert.ToInt32(reader["Salary"])
                            };
                        }
                    }
                }
            }
            return null;
        }

        // Update
        public async Task UpdateEmployeeAsync(string employeeCode, Employee updatedEmployee)
        {
            using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(winConnString))
            {
                string query = "UPDATE Employee SET EmployeeName = @EmpName, DepartmentCode = @DeptCode, " +
                               "LocationCode = @LocCode, Salary = @Sal WHERE EmployeeCode = @EmpCode";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@EmpCode", employeeCode);
                    command.Parameters.AddWithValue("@EmpName", updatedEmployee.EmployeeName);
                    command.Parameters.AddWithValue("@DeptCode", updatedEmployee.DepartmentCode);
                    command.Parameters.AddWithValue("@LocCode", updatedEmployee.LocationCode);
                    command.Parameters.AddWithValue("@Sal", updatedEmployee.Salary);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        // Delete
        public async Task DeleteEmployeeAsync(string employeeCode)
        {
            using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(winConnString))
            {
                string query = "DELETE FROM Employee WHERE EmployeeCode = @EmpCode";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@EmpCode", employeeCode);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        // List All Employees
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            var employees = new List<Employee>();

            using (SqlConnection conn = SqlServerConnectionManager.OpenConnection(winConnString))
            {
                string query = "SELECT * FROM Employee";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            employees.Add(new Employee
                            {
                                EmployeeCode = reader["EmployeeCode"].ToString(),
                                EmployeeName = reader["EmployeeName"].ToString(),
                                DepartmentCode = reader["DepartmentCode"].ToString(),
                                LocationCode = reader["LocationCode"].ToString(),
                                Salary = Convert.ToInt32(reader["Salary"])
                            });
                        }
                    }
                }
            }
            return employees;
        }
    }
}
