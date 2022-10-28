using System;
using System.Collections.Generic;

namespace Lab2
{
    public class Account
    {
        public readonly string UserName;
        protected uint CurrentRating = 1;
        protected virtual double RaitingWinCoef { get { return 1; } }
        protected virtual double RaitingLoseCoef { get { return 1; } }
        private int GamesCount { get { return GamesHistory.Count; } }
        private readonly List<Game> GamesHistory = new List<Game>();
        public virtual string AccountType { get { return "Default"; } }

        public Account(string userName)
        {
            UserName = userName;
        }

        public void WinGame(Game game)
        {
            CurrentRating += (uint)(game.Rating * RaitingWinCoef);
            GamesHistory.Add(game);
        }

        public void LoseGame(Game game)
        {
            if (CurrentRating <= game.Rating * RaitingLoseCoef)
                CurrentRating = 1;
            else
                CurrentRating -= (uint)(game.Rating * RaitingLoseCoef);
            GamesHistory.Add(game);
        }

        public void RecordGame(Game game)
        {
            if (game.Winner == this)
                CurrentRating += (uint)(game.Rating * RaitingWinCoef);
            else
                if (CurrentRating <= game.Rating * RaitingLoseCoef)
                CurrentRating = 1;
            else
                CurrentRating -= (uint)(game.Rating * RaitingLoseCoef);
            GamesHistory.Add(game);
        }

        public void GetStats()
        {
            Console.WriteLine($"Username: {UserName}");
            Console.WriteLine($"Account type: {AccountType}");
            Console.WriteLine($"Rating: {CurrentRating}");
            Console.WriteLine($"Games count: {GamesCount}");

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("|  #  |  Opponent  | Status | Rating |");
            Console.WriteLine("--------------------------------------");

            foreach (Game game in GamesHistory)
            {
                Account opponent = game.Winner == this ? game.Loser : game.Winner;
                string formattedName =
                    opponent.UserName.Length >= 10 ?
                    opponent.UserName.Substring(0, 7) + "..." :
                    opponent.UserName;
                Console.Write($"| {game.Id,3} | {formattedName,10} | ");
                ConsoleColor color = this == game.Winner ? ConsoleColor.Green : ConsoleColor.Red;
                Console.ForegroundColor = color;
                Console.Write($"{(color == ConsoleColor.Green ? "Win" : "Lose"),6}");
                Console.ResetColor();
                Console.Write(" | ");
                Console.ForegroundColor = color;
                Console.Write($"{(color == ConsoleColor.Green ? "+" + game.Rating * RaitingWinCoef : "-" + game.Rating * RaitingLoseCoef),6}");
                Console.ResetColor();
                Console.WriteLine(" |");
            }

            Console.WriteLine("--------------------------------------");
        }
    }

    public class BattlePassAccount : Account
    {
        public override string AccountType { get { return "Battle Pass"; } }
        protected override double RaitingWinCoef { get { return 1.5; } }
        protected override double RaitingLoseCoef { get { return 0.5; } }

        public BattlePassAccount(string userName) : base(userName)
        {
            CurrentRating = 100;
        }
    }

    public class LowPriorityAccount : Account
    {
        public override string AccountType { get { return "Low Priority"; } }
        protected override double RaitingWinCoef { get { return 0.5; } }
        protected override double RaitingLoseCoef { get { return 1; } }

        public LowPriorityAccount(string userName) : base(userName)
        { }
    }
}
