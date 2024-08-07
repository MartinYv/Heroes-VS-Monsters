namespace HeroesVSMonsters.GameModels.Contracts
{
    using System.ComponentModel.DataAnnotations;
    public abstract class Character
    {
        [Key]
        public int Id { get; set; }
        public int Strenght { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Range { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Damage { get; set; }
        public char Symbol { get; set; }
        static DateTime CreatedOn => DateTime.Now;

        public virtual void Setup()
        {
            Health = Strenght * 5;
            Mana = Intelligence * 3;
            Damage = Agility * 2;
        }

        public void BuffAbilitie(string abilitie, int ponts)
        {
            if (abilitie == "Strenght")
            {
                Strenght += ponts;
            }
            else if (abilitie == "Agility")
            {
                Agility += ponts;
            }
            else if (abilitie == "Intelligence")
            {
                Intelligence += ponts;
            }
            else
            {
                Console.WriteLine("Invalid input. Choose between- Strenght, Agility or Intelligence.");
            }
        }
    }
}

// Играчът може да си избере една между 3 различни раси: Warrior, Mage, Archer.
//o Mage: Strenght = 2; Agility = 1; Intelligence = 3; Range = 3; символ на полето: *
//o Warrior: Strenght = 3; Agility = 3; Intelligence = 0; Range = 1; символ на полето: @
//o Archer: Strenght = 2; Agility = 4; Intelligence = 0; Range = 2; символ на полето: #
//o За чудовищата: Strength, Agility, Intelligence са случайни между 1 и 3, Range е винаги 1;
//символ на полето: ◙

// Да съдържа 4 екрана (enum): MainMenu, CharacterSelect, InGame, Exit
// Нека всяка раса и чудовището да са в отделен клас
//Нека за всички да съдържат функция Setup()
//{
//    this.Health = this.Strenght * 5;
//    this.Mana = this.Intelligence * 3;
//    this.Damage = this.Agility * 2;
//}