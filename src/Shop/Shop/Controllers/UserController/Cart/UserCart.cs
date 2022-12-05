using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.UserLogic.Cart.Commands.Add;
using Application.UserLogic.Cart.Commands.Remove;
using Domain.Entities.Product;
using Domain.Entities.User;
using Application.Common.Help;
using System.Text.Json;
using Application.UserLogic.Cart.Queries.MyCart;

using System.Reflection;



namespace Shop.Controllers.UserController.Cart
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCart : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserCart(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet,Authorize(Roles ="User")]
        public async Task<ActionResult> userCart()
        {
            var (response,status) = await _mediator.Send(new MyCartQuery(Request.Cookies["token"]));
            return StatusCode(status,response);
        }

        [HttpPost,Authorize(Roles = "User")]
        [Route("add/{id:int}")]
        public async Task<ActionResult> Add(int id)
        {
            var token = Request.Cookies["token"];

            var addToCart = new AddCommand(token,id);

            var response = await _mediator.Send(addToCart);

            return StatusCode(response.statusCode,response);
        }

        [HttpDelete, Authorize(Roles = "User")]
        [Route("remove/{id:int}")]
        public async Task<ActionResult> Remove(int id)
        {
            string token = Request.Cookies["token"];
            var removeFromCart = new RemoveCommand(token,id);
            var response = await _mediator.Send(removeFromCart);

            return StatusCode(response.statusCode, response);
        }
    }
}
