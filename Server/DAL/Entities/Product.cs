using System.ComponentModel.DataAnnotations;

namespace Server.DAL.Entities;

public class Product
{
    [Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [MaxLength(128)]
    public required string Name { get; init; }

    public decimal Cost { get; init; }
}