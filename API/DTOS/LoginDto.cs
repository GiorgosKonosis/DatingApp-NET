using System;

namespace API.DTOS;

public class LoginDto
{
public required string Username { get; set; }

public required string Password { get; set; }
}
