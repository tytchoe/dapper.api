using api.dapper.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.dapper.Contracts
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrderMapping();
        Task<List<Order>> GetOrderLineMapping();
        Task<List<Order>> GetAllMapping(int id);
    }
}
