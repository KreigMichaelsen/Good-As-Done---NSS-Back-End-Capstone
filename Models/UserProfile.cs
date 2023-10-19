using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BackEndCapstone.Models;

public class UserProfile
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [NotMapped] // not mapped means that EF Core won't create column for this property in the db
    public string Email { get; set; }
    [NotMapped]
    public string UserName { get; set; }
    [NotMapped]
    public List<string> Roles { get; set; }

    public string IdentityUserId { get; set; }

    public IdentityUser IdentityUser { get; set; }

    public string FullName 

    {
        get 
        {
            return FirstName + " " + LastName;
        }
    }

    
}