using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.UserLogic.Balance.Commands.BalanceUp;
using Application.UserLogic.Balance.Commands.BalanceDown;
using Application.UserLogic.Balance.Queries;
using Application.Common.Help;
using Microsoft.AspNetCore.Authorization;
using MediatR;




namespace Shop.Controllers.UserController.Balance
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BalanceController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet, Authorize(Roles = "User")]
        
        public async Task<ActionResult> Balance()
        {
            var response = await _mediator.Send(new BalanceQuery(Request.Cookies["token"]));
            return StatusCode(response.statusCode,response);
        }

        [HttpPost,Authorize(Roles="User")]
        [Route("putMoney")]
        public async Task<ActionResult> BalanceUp(Money money)
        {
     
            var response = await _mediator.Send(new BalanceUpCommand(money.money, Request.Cookies["token"]));
            return StatusCode(response.statusCode, response);


        }

        [HttpPost, Authorize(Roles = "User")]
        [Route("getMoney")]
        public async Task<ActionResult> BalanceDown(Money money)
        {
            var response = await _mediator.Send(new BalanceDownCommand(money.money,Request.Cookies["token"]));
            return StatusCode(response.statusCode, response);



        }
        
    }
}
