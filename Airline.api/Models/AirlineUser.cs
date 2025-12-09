
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.Models;

public partial class AirlineUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AirlineUserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Document { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;
}
