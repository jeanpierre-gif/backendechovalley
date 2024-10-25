using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using training.DTOs;
using training.Models;
using training.Services.Account;

namespace training.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
       public IMediator mediator { get; set; }
        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ResponseModel<UserDTO>> Login(LoginDTO loginDTO)
        {
            return await mediator.Send(new LoginCommand { LoginDTO = loginDTO });
        }
    }
}
