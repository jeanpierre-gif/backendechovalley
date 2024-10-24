using MediatR;
using Microsoft.AspNetCore.Identity;
using training.DTOs;
using training.Models;

namespace training.Services.Account
{
    public class LoginCommand : IRequest<ResponseModel<UserDTO>>
    {
        public LoginDTO LoginDTO { get; set; }
        public class LoginCommandHandler : IRequestHandler<LoginCommand, ResponseModel<UserDTO>>
        {
            private readonly UserManager<UserModel> userManager;
            public LoginCommandHandler(UserManager<UserModel> userManager)
            {
                this.userManager = userManager;
            }
            public async Task<ResponseModel<UserDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var result = await userManager.FindByEmailAsync(request.LoginDTO.Email);
                if (result == null) {
                    return new ResponseModel<UserDTO>
                    {
                        status = false,
                        message="wrong email or password"
                    }; 
                }
                var checkpass = await userManager.CheckPasswordAsync(result,request.LoginDTO.Password);
                if (checkpass)
                {
                    return new ResponseModel<UserDTO>
                    {
                        status = true,
                        message = "success",
                        data = new UserDTO
                        {
                            Id = result.Id,
                            Email = result.Email,
                            UserName = result.UserName
                        }
                    };

                }
                return new ResponseModel<UserDTO>
                {
                    status = false,
                    message = "wrong email or pass"

                };
            }
        }
    }
    

}
