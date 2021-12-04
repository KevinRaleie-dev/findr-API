using System.ComponentModel.DataAnnotations;

namespace BursaryFinderAPI.Models;
public class User
{

    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName => FirstName + " " + LastName;

    public string Bio { get; set; }
    public string PhoneNumber { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

}