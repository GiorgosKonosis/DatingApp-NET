using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
 public static IServiceCollection AddIdentityServices(this IServiceCollection service,IConfiguration config)
 {
    service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>
    {
    var tokenKey = config["TokenKey"] ?? throw new Exception("Wrong Token Key");
    options.TokenValidationParameters = new TokenValidationParameters
        {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey    = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
        ValidateIssuer = false,
        ValidateAudience = false,

        };
    });
    return service;
 }
}