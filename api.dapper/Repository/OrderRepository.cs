using api.dapper.Context;
using api.dapper.Contracts;
using api.dapper.Dto;
using api.dapper.Entitites;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace api.dapper.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _context;

        public OrderRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrder(OrderForCreationDto order)
        {
            var query = "Insert into Orders(CustomerId,Status) VALUES (@CustomerId, @Status)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("CustomerId", order.CustomerId, DbType.Int32);
            parameters.Add("Status", order.Status, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdOrder = new Order
                {
                    Id = id,
                    CustomerId = order.CustomerId,
                    Status = order.Status,
                };

                return createdOrder;
            }
        }

        public async Task<List<Order>> GetAllMapping(int id)
        {
            var query = "SELECT o.id, o.Status, c.id, c.name, ol.id, ol.product FROM Orders o INNER JOIN OrderLines ol ON o.id = ol.OrderId INNER JOIN Customers c ON o.customerId = c.id WHERE o.id = @id";

            using (var connection = _context.CreateConnection())
            {
                var orderDict = new Dictionary<int, Order>();

                var orders = await connection.QueryAsync<Order,Customer, OrderLine, Order>(
                    query, (order, customer, orderline) =>
                    {
                        if (!orderDict.TryGetValue(order.Id, out var currentOrder))
                        {
                            currentOrder = order;
                            orderDict.Add(currentOrder.Id, currentOrder);
                        }
                           
                        currentOrder.Lines.Add(orderline);
                        order.Customers.Add(customer);
                        return currentOrder;
                    }
                ,param: new { id }
                );

                return orders.Distinct().ToList();
            }
        }

        public async Task<List<Order>> GetOrderLineMapping()
        {
            var query = "SELECT o.id, o.Status, ol.id, ol.product FROM Orders o INNER JOIN OrderLines ol ON o.id = ol.OrderId";

            using (var connection = _context.CreateConnection())
            {
                var orderDict = new Dictionary<int, Order>();

                var orders = await connection.QueryAsync<Order, OrderLine, Order>(
                    query, (order, orderline) =>
                    {
                        if (!orderDict.TryGetValue(order.Id, out var currentOrder))
                        {
                            currentOrder = order;
                            orderDict.Add(currentOrder.Id, currentOrder);
                        }

                        currentOrder.Lines.Add(orderline);
                        return currentOrder;
                    }
                );

                return orders.Distinct().ToList();
            }
        }

        public async Task<List<Order>> GetOrderMapping()
        {
            var query = "SELECT o.id, o.Status, c.id, c.name FROM Orders o INNER JOIN Customers c ON o.CustomerId = c.id";

            using (var connection = _context.CreateConnection())
            {
                var orderDict = new Dictionary<int, Order>();

                var orders = await connection.QueryAsync<Order, Customer, Order>(
                    query, (order, customer) =>
                    {
                        if (!orderDict.TryGetValue(order.Id, out var currentOrder))
                        {
                            currentOrder = order;
                            orderDict.Add(currentOrder.Id, currentOrder);
                        }

                        currentOrder.Customers.Add(customer);
                        return currentOrder;
                    }
                );

                return orders.Distinct().ToList();
            }
        }
    }
}
