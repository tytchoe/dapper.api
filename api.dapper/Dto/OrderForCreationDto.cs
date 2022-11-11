using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.dapper.Dto
{
    public class OrderForCreationDto
    {
        public int CustomerId { get; set; }
        public string Status { get; set; }
    }
}
