using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.UserLogic.UserAuthentication.Queries.LoginUser;
using Domain.Entities.User;
using Application.UserLogic.UserAuthentication.Commands.RegisterUser;
using Application.Common.Authentication.Cookie;
using Application.Common.Models;
using Application.Common.Help;
using static Application.Common.Help.Helper;
namespace Shop.Controllers.UserController.Authorization
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthorization : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserAuthorization(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        // ...............................................

        [HttpPost]
        [Route("Register")]
        public async Task<UserModel> Register(RegisterUserCommand command)
        {
            return await _mediator.Send(command);
        }

        // AUTHENTICATION </>/////////////////////////////////////

        [HttpPost]
        [Route("User/Login")]
        public async Task<Response<string>> Login([FromBody]UserLoginDTO user)
        {
            var (response,status) = await _mediator.Send(new LoginUserQuery(user.Username, user.Password));

            Response.Cookies.Append("token",
            response,
            new Cookies().Cookie(DateTime.Now.AddDays(1)));


           return Respond(response,status);
        }

        [HttpPost]
        [Route("Admin/Login")]
        public async Task<Response<string>> LoginAdmin([FromBody]UserLoginDTO admin) 
        {
            var (response,status) = await _mediator.Send(new LoginAdminQuery(admin.Username,admin.Password));
            Response.Cookies.Append("token", response, new Cookies().Cookie(DateTime.Now.AddDays(1)));
            return Respond(response,status);
        }

    }
}
