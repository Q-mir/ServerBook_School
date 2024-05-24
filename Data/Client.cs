using System.ComponentModel.DataAnnotations;

namespace Data;
public class Client
{
    [Key,Required]
    public int Id { get; set; }

    [MinLength(3)]
    [MaxLength(20)]
    public string Login { get; set; } = null!;

    [MinLength(3)]
    [MaxLength(15)]
    public string Password { get; set; } = null!;
}