using System;

namespace Lab2
{
    public enum Games
    {
        Training,
        Default,
        Challange
    }

    public class GameFactory
    {
        public static Game CreateGame(Games type, Account winner, Account loser, uint rating = 0)
        {
            return type switch
            {
                Games.Training => new TrainingGame(winner, loser),
                Games.Default => new DefaultGame(winner, loser, rating),
                Games.Challange => new ChallengeGame(winner, loser, rating),
                _ => new DefaultGame(winner, loser, rating),
            };
        }
    }

    public abstract class Game
    {
        static private uint _id = 0;
        public readonly uint Rating;
        public readonly uint Id = _id++;
        public readonly Account Winner;
        public readonly Account Loser;

        public Game(Account winner, Account loser, uint rating)
        {
            if (winner == loser) throw new ArgumentException("Opponents must be different");
            Winner = winner;
            Loser = loser;
            Rating = rating;

            winner.WinGame(this);
            loser.LoseGame(this);
        }
    }

    class TrainingGame : Game
    {
        public TrainingGame(Account winner, Account looser) : base(winner, looser, 0)
        { }
    }

    class DefaultGame : Game
    {
        public DefaultGame(Account winner, Account looser, uint rating) : base(winner, looser, rating)
        { }
    }

    class ChallengeGame : Game
    {
        public ChallengeGame(Account winner, Account looser, uint rating) : base(winner, looser, rating * 2)
        { }
    }
}
