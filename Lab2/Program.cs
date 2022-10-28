using System;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Account zxc = new Account("zxc1000-7deadinside");
            Account s1mple = new BattlePassAccount("s1mple");
            Account dendi = new LowPriorityAccount("Dendi");

            SimulateGame(Games.Default, s1mple, zxc, 10);
            SimulateGame(Games.Training, dendi, zxc, 10);
            SimulateGame(Games.Challange, dendi, s1mple, 10);

            zxc.GetStats();
            Console.WriteLine();
            s1mple.GetStats();
            Console.WriteLine();
            dendi.GetStats();
        }

        public static void SimulateGame(Games gameType, Account winner, Account loser, uint rating)
        {
            Game game = GameFactory.CreateGame(gameType, winner, loser, rating);
        }
    }
}
