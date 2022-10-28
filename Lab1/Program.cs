using System;
using System.Collections.Generic;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            GameAccount zxc = new GameAccount("zxc1000-7deadinside");
            GameAccount s1mple = new GameAccount("s1mple");
            Random random = new Random();

            zxc.WinGame(s1mple, (uint)random.Next(5, 30));
            s1mple.WinGame(zxc, (uint)random.Next(5, 30));
            zxc.LoseGame(s1mple, (uint)random.Next(5, 30));
            s1mple.LoseGame(zxc, (uint)random.Next(5, 30));

            zxc.GetStats();
            Console.WriteLine();
            s1mple.GetStats();
        }
    }
    class GameAccount
    {
        public readonly string UserName;
        private uint CurrentRating = 1;
        private uint GamesCount;
        private readonly List<Game> GamesHistory = new List<Game>();

        public GameAccount(string userName)
        {
            UserName = userName;
        }

        public void WinGame(GameAccount opponent, uint rating)
        {
            AddGame(new Game(this, opponent, rating));
        }

        public void LoseGame(GameAccount opponent, uint rating)
        {
            AddGame(new Game(opponent, this, rating));
        }

        public static void AddGame(Game game)
        {
            if (game.Winner == game.Loser) throw new ArgumentException("Opponent must be different from callable object");
            game.Winner.CurrentRating += game.Rating;
            if (game.Loser.CurrentRating <= game.Rating)
            {
                game.Loser.CurrentRating = 1;
            }
            else
            {
                game.Loser.CurrentRating -= game.Rating;
            }

            game.Winner.GamesCount++;
            game.Loser.GamesCount++;
            game.Winner.GamesHistory.Add(game);
            game.Loser.GamesHistory.Add(game);
        }

        public void GetStats()
        {
            Console.WriteLine($"Username: {UserName}");
            Console.WriteLine($"Rating: {CurrentRating}");
            Console.WriteLine($"Games count: {GamesCount}");

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("|  #  |  Opponent  | Status | Rating |");
            Console.WriteLine("--------------------------------------");

            foreach (Game game in GamesHistory)
            {
                GameAccount opponent = game.Winner == this ? game.Loser : game.Winner;
                string formattedName =
                    opponent.UserName.Length >= 10 ?
                    opponent.UserName.Substring(0, 7) + "..." :
                    opponent.UserName;
                Console.Write($"| {game.Id, 3} | {formattedName, 10} | ");
                ConsoleColor color = this == game.Winner ? ConsoleColor.Green : ConsoleColor.Red;
                Console.ForegroundColor = color;
                Console.Write($"{(color == ConsoleColor.Green ? "Win" : "Lose"), 6}");
                Console.ResetColor();
                Console.Write(" | ");
                Console.ForegroundColor = color;
                Console.Write($"{(color == ConsoleColor.Green ? "+" + game.Rating : "-" + game.Rating), 6}");
                Console.ResetColor();
                Console.WriteLine(" |");
            }

            Console.WriteLine("--------------------------------------");
        }
    }

    class Game
    {
        static private uint _id = 0;
        public readonly uint Rating;
        public readonly uint Id = _id++;
        public readonly GameAccount Winner;
        public readonly GameAccount Loser;

        public Game(GameAccount winner, GameAccount looser, uint rating)
        {
            Winner = winner;
            Loser = looser;
            Rating = rating;
        }

    }
}
