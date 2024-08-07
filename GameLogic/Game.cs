namespace GameLogic
{

    using global::Game.Enums;
    using HeroesVSMonsters.Data;
    using HeroesVSMonsters.Data.Models;
    using HeroesVSMonsters.GameModels;
    using HeroesVSMonsters.GameModels.Contracts;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
    public class Game
    {
        private Screen currentScreen;
        private readonly Database gameDb = new Database();

        public Game()
        {

        }

        public void Start()
        {
            currentScreen = Screen.MainMenu;

            while (currentScreen != Screen.Exit)
            {
                switch (currentScreen)
                {
                    case Screen.MainMenu:
                        MainMenu();
                        break;
                    case Screen.CharacterSelect:
                        CharacterSelect();
                        break;
                    case Screen.InGame:
                        InGame();
                        break;
                    case Screen.Exit:
                        ExitScreen();
                        break;
                }
            }
        }
        private void MainMenu()
        {
            Console.WriteLine("Welcome!");
            Console.WriteLine("Press any key to play.");

            Console.ReadKey();
            currentScreen = Screen.CharacterSelect;
        }
        private void CharacterSelect()
        {
            Console.Clear();

            Character character = null!;
            int remainingBuffPoints = 3;

            Console.WriteLine("Choose character type:");
            Console.WriteLine("Options:");
            Console.WriteLine("1) Warrior");
            Console.WriteLine("2) Archer");
            Console.WriteLine("3) Mage");
            Console.WriteLine("Your pick:");

            string characterChoice = Console.ReadLine()!;

            //While we succsessfully choose a character                  
            while (character == null)
            {
                if (characterChoice.IsNullOrEmpty() || IsItNumber(characterChoice) == false)
                {
                    Console.WriteLine("Write a valid input.");
                    characterChoice = Console.ReadLine()!;
                    continue;
                }

                if (int.Parse(characterChoice) == 1)
                {
                    character = new Warrior();
                }
                else if (int.Parse(characterChoice) == 2)
                {
                    character = new Archer();
                }
                else if (int.Parse(characterChoice) == 3)
                {
                    character = new Mage();
                }
                else
                {
                    Console.WriteLine("Invalid input. Choose between 1, 2 or 3.");

                    characterChoice = Console.ReadLine()!;
                    continue;
                }
            }

            Console.Clear();

            Console.WriteLine($"Would you like to buff up your stats before starting? (Limit: 3 points total)");
            Console.WriteLine("Response (Y\\N):");

            string buffResponce = Console.ReadLine()!.ToUpper();
            bool buffsAdded = false;

            while (buffResponce != "Y" || buffResponce != "N")
            {
                if (remainingBuffPoints == 0 || buffsAdded == true)
                {
                    break;
                }

                if (buffResponce == "Y")
                {
                    //Choosing abilities
                    for (int i = 1; i <= 3; i++)
                    {
                        Console.Clear();
                        Console.WriteLine($"Remaining Points: {remainingBuffPoints}");

                        if (remainingBuffPoints == 0)
                        {
                            break;
                        }

                        //Strength
                        if (i == 1)
                        {
                            Console.WriteLine("Add to Strenght:");
                            string strenghtPoints = Console.ReadLine()!;

                            //While we recieve valid strength integer
                            while (true)
                            {
                                if (IsItNumber(strenghtPoints))
                                {
                                    int points = int.Parse(strenghtPoints);

                                    if (points <= remainingBuffPoints && points >= 0)
                                    {
                                        character.BuffAbilitie("Strenght", points);
                                        remainingBuffPoints -= points;

                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Write a valid number between 1-3 .");
                                        strenghtPoints = Console.ReadLine()!;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"You need to write a number.");
                                    strenghtPoints = Console.ReadLine()!;
                                }
                            }
                        }
                        else if (i == 2) //Add to Agility
                        {
                            Console.WriteLine("Add to Agility:");
                            string agilityPoints = Console.ReadLine()!;

                            //While we recieve valid Agility integer
                            while (true)
                            {
                                if (IsItNumber(agilityPoints))
                                {
                                    int points = int.Parse(agilityPoints);

                                    if (points <= remainingBuffPoints && points >= 0)
                                    {
                                        character.BuffAbilitie("Agility", points);
                                        remainingBuffPoints -= points;

                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Write a valid number between 1-3 .");
                                        agilityPoints = Console.ReadLine()!;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"You need to write a number.");
                                    agilityPoints = Console.ReadLine()!;
                                }
                            }
                        }
                        else // Add to Intelligence
                        {
                            Console.WriteLine("Add to Intelligence:");
                            string intelligencePoints = Console.ReadLine()!;

                            //While we recieve valid Intelligence integer
                            while (true)
                            {
                                if (IsItNumber(intelligencePoints))
                                {
                                    int points = int.Parse(intelligencePoints);

                                    if (points <= remainingBuffPoints && points >= 0)
                                    {
                                        character.BuffAbilitie("Intelligence", points);
                                        remainingBuffPoints -= points;

                                        buffsAdded = true;

                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Write a valid number between 1-3 .");
                                        intelligencePoints = Console.ReadLine()!;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"You need to write a number.");
                                    intelligencePoints = Console.ReadLine()!;
                                }
                            }
                        }
                    }
                }
                else if (buffResponce == "N")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Choose between 'Y' or 'N'");
                    buffResponce = Console.ReadLine()!.ToUpper();
                }
            }

            AddPlayer(character);

            Console.Clear();

            currentScreen = Screen.InGame;
        }
        public void InGame()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<Monster> monsters = new List<Monster>();

            Player player = gameDb.Players.OrderBy(p => p.CreatedOn)
                       .LastOrDefault()!;

            var field = CraeteField(player!.Symbol);

            int playerRow = 1;
            int playerCol = 1;
            int fieldSize = field.GetLength(0);

            PrintField(field, player);

            while (player.Health > 0)
            {
                Console.WriteLine("Choose action:");
                Console.WriteLine("1) Attack");
                Console.WriteLine("2) Move");

                string action = Console.ReadLine()!;

                if (!IsItNumber(action))
                {
                    Console.WriteLine("Wrong input. You need to insert number.");

                    Thread.Sleep(3000);
                    Console.Clear();

                    PrintField(field, player);
                    continue;
                }
                if (int.Parse(action) != 1 && int.Parse(action) != 2)
                {
                    Console.WriteLine("Invalid action. You need to choose between 1 or 2.");

                    Thread.Sleep(3000);
                    Console.Clear();

                    PrintField(field, player);
                    continue;
                }

                if (int.Parse(action) == 1) // Attack
                {
                    List<Monster> monstersInRange = CheckForMonstersInRange(player.Range, playerRow, playerCol, monsters);

                    if (monstersInRange.Count != 0) // If there are available mosnters
                    {
                        for (int i = 0; i < monstersInRange.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}) Target with remaining blood {monstersInRange[i].Health}");
                        }

                        Console.WriteLine("Which one to attack:");

                        string monsterChoice = Console.ReadLine()!;

                        if (IsItNumber(monsterChoice) && !monsterChoice.IsNullOrEmpty() &&
                            int.Parse(monsterChoice) <= monstersInRange.Count && int.Parse(monsterChoice) >= 1)
                        {
                            int monsterIndex = int.Parse(monsterChoice) - 1;

                            if (player.Damage >= monstersInRange[monsterIndex].Health)
                            {
                                Console.WriteLine("You killed the monster.");

                                field[monstersInRange[monsterIndex].Row, monstersInRange[monsterIndex].Column] = '▒';

                                Thread.Sleep(2000);

                                monsters.Remove(monstersInRange[monsterIndex]);
                            }
                            else
                            {
                                Console.WriteLine($"You attacked monster with {monstersInRange[monsterIndex].Health}, but you couldn't killed him.");

                                Thread.Sleep(2000);
                                monstersInRange[monsterIndex].Health -= player.Damage;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Not valid monster number.");
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("No available targets in your range");
                        continue;
                    }
                }
                else if (int.Parse(action) == 2) // Moove
                {
                    List<string> validDirections = new List<string>() { "W", "S", "D", "A", "E", "X", "Q", "Z" };

                    Console.WriteLine("Choose direction:");

                    string mooveDirection = Console.ReadLine()!.ToUpper();

                    if (!validDirections.Any(x => x == mooveDirection))
                    {
                        Console.WriteLine("Invalid direction. Choose between \"W\", \"S\", \"D\", \"A\", \"E\", \"X\", \"Q\", \"Z\" ");
                        Thread.Sleep(2000);
                        continue;
                    }

                    bool isMoveSuccessfull = HandlePlayerMove(mooveDirection, ref playerRow, ref playerCol, player.Range, ref field, player);

                    if (!isMoveSuccessfull)
                    {
                        continue;
                    }
                }

                MoveMonsters(field, monsters, playerRow, playerCol, player);
                SpawnMonster(monsters, field, player.Symbol);

                Console.Clear();
                PrintField(field, player);
            }
        }
        private void ExitScreen()
        {
            Console.WriteLine("You were killed by a monster.");
            Console.WriteLine("Game Over");

            Thread.Sleep(1500);

            Console.Clear();

            Console.WriteLine("Do you want to start a new game?");
            Console.WriteLine("Response (Y\\N):");

            string response = Console.ReadLine()!;

            while (true)
            {
                if (response.ToUpper() == "Y")
                {
                    currentScreen = Screen.MainMenu;
                    Start();
                }
                else if (response.ToUpper() == "N")
                {
                    Console.WriteLine("Press any key to close the game.");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("Write a valid input (Y or N)");
                    response = Console.ReadLine()!;
                }
            }
        }
        private async void AddPlayer(Character character)
        {
            Player player = new Player()
            {
                HeroType = character.GetType().Name,
                Agility = character.Agility,
                Damage = character.Damage,
                Health = character.Health,
                Intelligence = character.Intelligence,
                Mana = character.Mana,
                Range = character.Range,
                Strenght = character.Strenght,
                Symbol = character.Symbol,
                CreatedOn = DateTime.Now
            };

            await gameDb.Players.AddAsync(player);
            await gameDb.SaveChangesAsync();
        }
        private static bool HandlePlayerMove(string mooveDirection, ref int playerRow, ref int playerCol, int range, ref char[,] field, Player player)
        {
            int newPLayerRow = playerRow;
            int newPlayerCol = playerCol;

            if (mooveDirection == "W")
            {
                newPLayerRow -= range;
            }
            else if (mooveDirection == "S")
            {
                newPLayerRow += range;
            }
            else if (mooveDirection == "D")
            {
                newPlayerCol += range;
            }
            else if (mooveDirection == "A")
            {
                newPlayerCol -= range;
            }
            else if (mooveDirection == "Q")
            {
                newPLayerRow -= range;
                newPlayerCol -= range;
            }
            else if (mooveDirection == "E")
            {
                newPLayerRow -= range;
                newPlayerCol += range;
            }
            else if (mooveDirection == "Z")
            {
                newPLayerRow += range;
                newPlayerCol -= range;
            }
            else if (mooveDirection == "X")
            {
                newPLayerRow += range;
                newPlayerCol += range;
            }

            if (IsIndexValid(newPLayerRow, newPlayerCol, field.GetLength(0)))
            {
                if (!IsThereMonster(newPLayerRow, newPlayerCol, field))
                {
                    MovePlayer(newPLayerRow, newPlayerCol, field, player.Symbol);

                    field[playerRow, playerCol] = '▒';

                    playerRow = newPLayerRow;
                    playerCol = newPlayerCol;

                    return true;
                }
                else
                {
                    Console.WriteLine("There is a monster, you can't move there.");
                    Thread.Sleep(1500);
                    Console.Clear();
                    PrintField(field, player);
                    return false;
                }
            }
            else
            {
                Console.WriteLine("You can't go out of the field. Choose valid move.");
                Thread.Sleep(1500);
                Console.Clear();
                PrintField(field, player);
                return false;
            }
        }
        private static void SpawnMonster(List<Monster> monsters, char[,] field, char playerSymbol)
        {
            while (true)
            {
                Random random = new Random();

                int monsterRow = random.Next(0, 10);
                int monsterColl = random.Next(0, 10);

                if (field[monsterRow, monsterColl] == '▒')
                {
                    Monster monster = new Monster();

                    monster.Row = monsterRow;
                    monster.Column = monsterColl;

                    monsters.Add(monster);

                    field[monsterRow, monsterColl] = monster.Symbol;

                    break;
                }
            }
        }
        private void MoveMonsters(char[,] field, List<Monster> monsters, int playerRow, int playerCol, Player player)
        {
            foreach (var monster in monsters)
            {
                int monsterPreviousRow = monster.Row;
                int monsterPreviousCol = monster.Column;

                if (monster.Row > playerRow)
                {
                    monster.Row--;
                }
                else if (monster.Row < playerRow)
                {
                    monster.Row++;
                }

                if (monster.Column > playerCol)
                {
                    monster.Column--;
                }
                else if (monster.Column < playerCol)
                {
                    monster.Column++;
                }

                //if the new position is not the position of another monster or the player
                if (field[monster.Row, monster.Column] != monster.Symbol && field[monster.Row, monster.Column] != player.Symbol)
                {
                    field[monsterPreviousRow, monsterPreviousCol] = '▒';
                    field[monster.Row, monster.Column] = monster.Symbol;
                }
                else
                {
                    monster.Row = monsterPreviousRow;
                    monster.Column = monsterPreviousCol;
                }

                if (Math.Abs(monster.Row - playerRow) <= 1 && Math.Abs(monster.Column - playerCol) <= 1)
                {
                    if (monster.Damage >= player.Health)
                    {
                        player.Health = 0;

                        currentScreen = Screen.Exit;

                        Console.Clear();
                        PrintField(field, player);
                        ExitScreen();
                        break;
                    }
                    else
                    {
                        player.Health -= monster.Damage;

                        Console.WriteLine($"You were attacked by a monster! Available health {player.Health}.");
                        Thread.Sleep(1500);
                        Console.Clear();
                        PrintField(field, player);
                    }
                }
            }
        }
        private static List<Monster> CheckForMonstersInRange(int range, int playerRow, int playerCol, List<Monster> monsters)
        {
            List<Monster> availableMonsters = new List<Monster>();

            foreach (var monster in monsters.Where(m => m.Row == playerRow + range && m.Column == playerCol ||
                                                   m.Row == playerRow - range && m.Column == playerCol ||
                                                   m.Row == playerRow && m.Column == playerCol + range ||
                                                   m.Row == playerRow && m.Column == playerCol - range ||
                                                   m.Row == playerRow - range && m.Column == playerCol - range ||
                                                   m.Row == playerRow - range && m.Column == playerCol + range ||
                                                   m.Row == playerRow + range && m.Column == playerCol - range ||
                                                   m.Row == playerRow + range && m.Column == playerCol + range))
            {

                availableMonsters.Add(monster);
            }

            return availableMonsters;
        }
        private static bool IsThereMonster(int row, int col, char[,] field)
        {
            return field[row, col] == '◙';
        }
        private static void MovePlayer(int row, int col, char[,] field, char playerSymbol)
        {
            field[row, col] = playerSymbol;
        }
        private static void PrintField(char[,] field, Player player)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Health: {player.Health}  Mana: {player.Mana}");
            sb.AppendLine();

            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    sb.Append(field[y, x].ToString().PadRight(2));
                }
                sb.AppendLine();
            }
            Console.WriteLine(sb.ToString());
        }
        private static char[,] CraeteField(char playerSymbol)
        {
            char[,] field = new char[10, 10];

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    field[row, col] = '▒';
                }

                Console.Write("");
            }

            const int playerStartPositionRow = 1;
            const int playerStartPositionColl = 1;

            field[playerStartPositionRow, playerStartPositionColl] = playerSymbol;

            return field;
        }
        public static bool IsItNumber(string input)
        {
            int number;

            bool success = int.TryParse(input, out number);

            return success;
        }
        private static bool IsIndexValid(int row, int col, int fieldSize)
        {
            return row >= 0 && row < fieldSize && col >= 0 && col < fieldSize;
        }
    }
}