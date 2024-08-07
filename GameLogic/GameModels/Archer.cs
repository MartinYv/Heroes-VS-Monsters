namespace HeroesVSMonsters.GameModels
{
    using HeroesVSMonsters.GameModels.Contracts;

    public class Archer : Character
    {
        private const int ARCHER_STRENGHT = 2;
        private const int ARCHER_AGILITY = 4;
        private const int ARCHER_INTELIGENCE = 0;
        private const int ARCHER_RANGE = 2;
        private const char ARCHER_SYMBOL = '#';
        public Archer()
        {
            Strenght = ARCHER_STRENGHT;
            Agility = ARCHER_AGILITY;
            Intelligence = ARCHER_INTELIGENCE;
            Range = ARCHER_RANGE;
            Symbol = ARCHER_SYMBOL;

            Setup();
        }
    }
}

//o Archer: Strenght = 2; Agility = 4; Intelligence = 0; Range = 2; символ на полето: #