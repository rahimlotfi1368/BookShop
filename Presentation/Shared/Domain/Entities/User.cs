using System.ComponentModel.DataAnnotations;
using Presentation.Shared.Domain.Entities.Common;

namespace Presentation.Shared.Domain.Entities;

public class User:BaseEntity
{
    public string Username { get; set; }
    
    public string PasswordHash { get; set; }
   
    public string Role { get; set; }
}