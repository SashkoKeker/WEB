using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB.DTO;
using WEB.Models;
using WEB.Services;
using WEB.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        

        public OrderController(IOrderService orderservice, IMapper mapper)
        {
            _orderService = orderservice;
            _mapper = mapper;
        }
        [HttpGet("Read")]
        public IActionResult GetAll()
        {
            var users = _orderService.GetAll();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        [Authorize]
        [HttpPost("Create")]
        public IActionResult CreateOrder(OrderDTO Order)
        {
            var req = _mapper.Map<Order>(Order);
            return Created("", _orderService.CreateOrder(req));
        }

        [HttpPatch("Update")]
        public IActionResult UpdateOrder(OrderDTO order)
        {
            var req = _mapper.Map<Order>(order);
            return StatusCode((int)HttpStatusCode.NoContent, _orderService.UpdateOrder(req));
        }

        [HttpGet("My/{id}")]
        public IActionResult GetMy(Guid id)
        {
            var purchasePost = _orderService.GetById(id);
            return Ok(_mapper.Map<IEnumerable<OrderDTO>>(purchasePost));
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteReq(Guid id)
        {
            _orderService.Delete(id);
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        [HttpGet("Type/{id}")]
        public IActionResult GetByType(int Type)
        {
            var purchasePost = _orderService.GetByType(Type);
            return Ok(_mapper.Map<IEnumerable<OrderDTO>>(purchasePost));
        }
    }
}