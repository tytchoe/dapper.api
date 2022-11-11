using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.dapper.Contracts;
using api.dapper.Entitites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.dapper.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeesController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _employeeRepo.GetEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}",Name = "EmployeeById")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            try
            {
                var employee = await _employeeRepo.GetEmployee(id);
                if (employee == null)
                    return NotFound();

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("age/{age}", Name = "EmployeeByAge")]
        public async Task<IActionResult> GetEmployees(int age)
        {
            try
            {
                var employees = await _employeeRepo.GetEmployees(age);
                if (employees == null)
                    return NotFound();

                return Ok(employees);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("SBCI/{id}",Name = "SearchEmployeeByCompanyId")]
        public async Task<IActionResult> GetEmployeesByCompanyId(int id)
        {
            try
            {
                var employees = await _employeeRepo.GetEmployeesByCompanyId(id);
                if (employees == null)
                    return NotFound();
                return Ok(employees);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}