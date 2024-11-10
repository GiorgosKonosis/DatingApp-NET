


using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOS;
using API.Entities;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;



public class AccountController(DataContext context, ITokenService tokenService):BaseApiController{


    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto){

        if(await SameUser(registerDto.UserName)) return BadRequest("Username already exists");

        using var hmac =new HMACSHA512();
        var user = new AppUser(){
            UserName=registerDto.UserName.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

         context.Users.Add(user);
        await context.SaveChangesAsync();

        return new UserDto{
            Username = user.UserName,
            Token = tokenService.GetToken(user)
        };

    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){
        
        var user = await context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
        
        if(user == null) return Unauthorized("Invalid username or password");

        using var hmac = new HMACSHA512(user.PasswordSalt);

         var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

      if (!computedHash.SequenceEqual(user.PasswordHash)) return Unauthorized("Invalid username or password");
            
        

        return new  UserDto{
            Username = user.UserName,
            Token = tokenService.GetToken(user)

        };

    }

    public async Task<bool> IsPassword(string username,string password){
        var hmac = new HMACSHA3_512();
        var pass = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return await context.Users.AnyAsync(x=> x.UserName.ToLower() == username.ToLower() && x.PasswordHash ==  pass);
    }

    public async  Task<bool> SameUser(string username){

            return await context.Users.AnyAsync(x=>x.UserName.ToLower() == username.ToLower());

    }
}

