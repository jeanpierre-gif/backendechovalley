using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;  
using Microsoft.IdentityModel.Tokens;  
using System.Text;
using training.Context;
using training.Models;
using training.Services.Token;

namespace training.Extensions  
{
    public static class IdentityServiceExtensions  
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            // Setting up user identity with custom user model and pass requirements
            services.AddIdentityCore<UserModel>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;  // allow passwords without special characters
                options.Password.RequiredLength = 8;  // minimum password length of 8 characters
                options.User.RequireUniqueEmail = true;  // each email must be unique
            })
            .AddEntityFrameworkStores<MyDbContext>()  // use MyDbContext to store identity data
            .AddDefaultTokenProviders();  // add default token providers for identity

            // Create a security key from the configuration token key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));

            // Set up JWT authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,  // check the token's signing key
                        IssuerSigningKey = key,  // use the generated security key
                        ValidateIssuer = false,  // no need to verify issuer
                        ValidateAudience = false  // no need to verify audience
                    };
                });

            // Add TokenService for handling token-related operations
            services.AddScoped<TokenService>();

            return services;  // return the modified service collection
        }
    }
}
