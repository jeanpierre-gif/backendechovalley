using MediatR;
using Microsoft.AspNetCore.Mvc;
using training.DTOs;
using training.Models;
using training.Services.Account;

namespace training.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;
        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("Login")]
        public async Task<ResponseModel<UserDTO>> Login(LoginDTO loginDTO)
        {
            return await mediator.Send(new LoginCommand { LoginDTO = loginDTO });   
        }
    }
}
