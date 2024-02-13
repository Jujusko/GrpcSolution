using System.ComponentModel.DataAnnotations;

namespace Server.DAL.Entities
{
    public class Product
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Cost { get; set; }
    }
}
