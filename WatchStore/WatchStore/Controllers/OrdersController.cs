using Business;
using DTOs;
using Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WatchStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrdersServices _ordersServices;

        public OrdersController(IOrdersServices ordersServices)
        {
            _ordersServices = ordersServices;
        }
        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> Get()
        {
            return await _ordersServices.GetOrders();
        }

        // GET api/<OrdersController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<OrdersController>
        [HttpPost]
        public async Task Post([FromBody]OrderDto orderDto)
        {
            await _ordersServices.AddOrder(orderDto);
        }

        // PUT api/<OrdersController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<OrdersController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
