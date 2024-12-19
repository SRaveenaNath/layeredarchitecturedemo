using ConsoleEmployeeAppCRUD.Model;
using ConsoleEmployeeAppCRUD.Repository;
using ConsoleEmployeeAppCRUD.Service;
using layeredarchitecturedemo.Model;
using layeredarchitecturedemo.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace layeredarchitecturedemo.Service
{
    public class EmployeeServiceImpl : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeServiceImpl(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _employeeRepository.AddEmployeeAsync(employee);
        }

        public async Task UpdateEmployeeAsync(string employeeCode, Employee updatedEmployee)
        {
            await _employeeRepository.UpdateEmployeeAsync(employeeCode, updatedEmployee);
        }

        public async Task<Employee> GetEmployeeByCodeAsync(string employeeCode)
        {
            return await _employeeRepository.GetEmployeeByCodeAsync(employeeCode);
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllEmployeesAsync();
        }

        public async Task DeleteEmployeeAsync(string employeeCode)
        {
            await _employeeRepository.DeleteEmployeeAsync(employeeCode);
        }
    }
}
