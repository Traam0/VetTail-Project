using Microsoft.AspNetCore.Identity;
using System;

namespace VetTail.Infrastructure.Identity;

public class User : IdentityUser
{
    public DateTimeOffset CreatedAT { get; set; } = DateTimeOffset.UtcNow;
}
