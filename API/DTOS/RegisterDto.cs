using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOS;

public class RegisterDto
{
    [Required]
    public  string UserName { get; set; } =string.Empty;
    [Required]
    [StringLength(32,MinimumLength = 4)]
    public  string Password { get; set; } = string.Empty;
}
