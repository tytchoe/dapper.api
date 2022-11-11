using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.dapper.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.dapper.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;

        public OrdersController(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        [HttpGet("getall", Name = "Getall")]
        public async Task<IActionResult> GetOrderMapping()
        {
            try
            {
                var order = await _orderRepo.GetOrderMapping();

                return Ok(order);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("getline", Name = "Getline")]
        public async Task<IActionResult> GetOrderLineMapping()
        {
            try
            {
                var order = await _orderRepo.GetOrderLineMapping();

                return Ok(order);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get/{id}", Name = "get")]
        public async Task<IActionResult> GetAllMapping(int id)
        {
            try
            {
                var order = await _orderRepo.GetAllMapping(id);

                return Ok(order);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}