namespace HeroesVSMonsters.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Player
    {
        [Key]
        public int Id { get; set; }
        public string HeroType { get; set; } = null!;
        public int Strenght { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Range { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Damage { get; set; }
        public char Symbol { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}