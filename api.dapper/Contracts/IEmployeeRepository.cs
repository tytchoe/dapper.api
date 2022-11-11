using api.dapper.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.dapper.Contracts
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployee(int id);
        Task<IEnumerable<Employee>> GetEmployees(int age);
        Task<IEnumerable<Employee>> GetEmployeesByCompanyId(int companyId);
    }
}
