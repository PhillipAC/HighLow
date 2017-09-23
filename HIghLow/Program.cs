using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighLow
{
    /// <summary>
    /// Plays a game of High/Low as long as the user wantsdcsw
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Keep playing as long as true
            bool playAgain = true;

            //Play game
            while (playAgain)
            {
                Game game = new Game(1, 100);
                game.Title();
                game.Play();
                playAgain = game.ConfirmAgain();
            }
            //Thank user for playing
            Console.WriteLine("Thank you for playing.");
            Console.WriteLine("Press any key to exit the game");
            Console.ReadKey();
        }
    }
}
