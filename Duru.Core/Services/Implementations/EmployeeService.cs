using Duru.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duru.Core.Repositories;

namespace Duru.Core.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            var entities = await _employeeRepository.GetAllAsync();
            return entities.Select(MapToModel);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var entity = await _employeeRepository.GetByIdAsync(id);
            return entity != null ? MapToModel(entity) : null;
        }

        public async Task<int> CreateEmployeeAsync(Employee employee)
        {
            var entity = MapToEntity(employee);
            entity.CreatedAt = DateTime.Now;
            return await _employeeRepository.InsertAsync(entity);
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            var entity = MapToEntity(employee);
            var result = await _employeeRepository.UpdateAsync(entity);
            return result;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var result = await _employeeRepository.DeleteAsync(id);
            return result;
        }

        private Employee MapToModel(Employee entity)
        {
            return new Employee
            {
                EmployeeId = entity.EmployeeId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Position = entity.Position,
                Email = entity.Email,
                Phone = entity.Phone,
                HireDate = entity.HireDate,
                Status = entity.Status,
                CreatedAt = entity.CreatedAt
            };
        }

        private Employee MapToEntity(Employee model)
        {
            return new Employee
            {
                EmployeeId = model.EmployeeId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Position = model.Position,
                Email = model.Email,
                Phone = model.Phone,
                HireDate = model.HireDate,
                Status = model.Status,
                CreatedAt = model.CreatedAt
            };
        }
    }
}
