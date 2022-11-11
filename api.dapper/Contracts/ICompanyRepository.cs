using api.dapper.Dto;
using api.dapper.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.dapper.Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompanies();
        Task<Company> GetCompany(int id);
        Task<Company> CreateCompany(CompanyForCreationDto company);
        Task UpdateCompany(int id, CompanyForUpdateDto company);
        Task DeleteCompany(int id);
        Task<Company> GetCompanyByEmployeeId(int id);
        Task<Company> GetCompanyEmployeesMultipleResults(int id);
        Task<List<Company>> GetCompaniesEmployeesMultipleMapping();
        Task DeleteCompanyUpdate(int id);
    }
}
