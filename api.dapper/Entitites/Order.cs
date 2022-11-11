    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.dapper.Entitites
{
    public class Order
    {
        public int Id { get; set; }
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<OrderLine> Lines { get; set; } = new List<OrderLine>();
        public string Status { get; set; }
    }
}
