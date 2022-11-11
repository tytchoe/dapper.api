using api.dapper.Context;
using api.dapper.Contracts;
using api.dapper.Entitites;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace api.dapper.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;

        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            var query = "Select * from Employees Where Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var employee = await connection.QuerySingleOrDefaultAsync<Employee>(query, new { id });
                return employee;
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var query = "Select * from Employees";

            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Employee>(query);
                return employees.ToList();
            }

        }

        public async Task<IEnumerable<Employee>> GetEmployees(int age)
        {
            var query = "Select * from Employees Where Age >= @Age";
            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Employee>(query, new { age });
                return employees.ToList();
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByCompanyId(int companyId)
        {
            var procedureName = "SelectEmployeeByCompany";
            var parameter = new DynamicParameters();
            parameter.Add("Id", companyId, DbType.Int32, ParameterDirection.Input);
            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Employee>
                    (procedureName,parameter,commandType : CommandType.StoredProcedure);

                return employees.ToList();
            }
        }
    }
}
