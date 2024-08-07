namespace HeroesVSMonsters.GameModels
{
    using HeroesVSMonsters.GameModels.Contracts;

    public class Monster : Character
    {
        private const int MONSTER_RANGE = 1;
        private const char MONSTER_SYMBOL = '◙';

        public int Row { get; set; }
        public int Column { get; set; }
        public Monster()
        {
            Range = MONSTER_RANGE;
            Symbol = MONSTER_SYMBOL;

            Setup();
        }

        public override void Setup()
        {
            Random random = new Random();

            List<int> randomAbilitiePoints = new List<int>();

            for (int i = 0; i < 3; i++)
            {
                randomAbilitiePoints.Add(random.Next(1, 4));
            }

            Strenght = randomAbilitiePoints[0];
            Agility = randomAbilitiePoints[1];
            Intelligence = randomAbilitiePoints[2];

            base.Setup();
        }
    }
}