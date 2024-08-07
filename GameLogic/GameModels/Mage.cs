namespace HeroesVSMonsters.GameModels
{
    using HeroesVSMonsters.GameModels.Contracts;

    public class Mage : Character
    {
        private const int MAGE_STRENGHT = 2;
        private const int MAGE_AGILITY = 1;
        private const int MAGE_INTELIGENCE = 3;
        private const int MAGE_RANGE = 3;
        private const char MAGE_SYMBOL = '*';
        public Mage()
        {
            Strenght = MAGE_STRENGHT;
            Agility = MAGE_AGILITY;
            Intelligence = MAGE_INTELIGENCE;
            Range = MAGE_RANGE;
            Symbol = MAGE_SYMBOL;

            Setup();
        }
    }
}

//o Mage: Strenght = 2; Agility = 1; Intelligence = 3; Range = 3; символ на полето: *
