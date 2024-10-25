using MediatR;
using Microsoft.AspNetCore.Identity;
using training.DTOs;
using training.Models;
using training.Services.Token;

namespace training.Services.Account
{
    public class LoginCommand : IRequest<ResponseModel<UserDTO>>
    {
        public LoginDTO LoginDTO { get; set; }
        public class LoginCommandHandler : IRequestHandler<LoginCommand, ResponseModel<UserDTO>>
        {
            private UserManager<UserModel> userManager;
            private TokenService tokenService;
            public LoginCommandHandler(UserManager<UserModel> userManager, TokenService tokenService)
            {
                this.tokenService= tokenService;
                this.userManager = userManager;
           
            }

            public async Task<ResponseModel<UserDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var result = await userManager.FindByEmailAsync(request.LoginDTO.email);
                if (result == null) {
                    return new ResponseModel<UserDTO>
                    {
                        status = false,
                        message = "wrong email or password",
                    };
                }
                var checkpass= await userManager.CheckPasswordAsync(result,request.LoginDTO.password);
                if (checkpass)
                {
                    return new ResponseModel<UserDTO>
                    {
                        status = true,
                        message = "success",
                        data = new UserDTO
                        {
                            Id = result.Id,
                            email = result.Email,
                            token = tokenService.CreateToken(result)
                        }
                    };
                }
                return new ResponseModel<UserDTO>
                {
                    status = false,
                    message = "wrong email or password"
                };
           
            }
        }
    }
}
