namespace HeroesVSMonsters.GameModels
{
    using HeroesVSMonsters.GameModels.Contracts;

    public class Warrior : Character
    {
        private const int WARRIOR_STRENGHT = 3;
        private const int WARRIOR_AGILITY = 3;
        private const int WARRIOR_INTELIGENCE = 0;
        private const int WARRIOR_RANGE = 1;
        private const char WARRIOR_SYMBOL = '@';

        public Warrior()
        {
            Strenght = WARRIOR_STRENGHT;
            Agility = WARRIOR_AGILITY;
            Intelligence = WARRIOR_INTELIGENCE;
            Range = WARRIOR_RANGE;
            Symbol = WARRIOR_SYMBOL;

            Setup();
        }
    }
}

//o Warrior: Strenght = 3; Agility = 3; Intelligence = 0; Range = 1; символ на полето: @
