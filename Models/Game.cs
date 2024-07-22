using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class Game : BaseEntity
    {

        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Cover { get; set; } = string.Empty;

        public int CategoryId { get; set; } //FK , One Game to one category && one category to many Games 
        public Category Category { get; set; } = default! ;

        public ICollection<GameDevice> Devices { get; set; } = new List<GameDevice>() ; // many to many relation 
    }
}
