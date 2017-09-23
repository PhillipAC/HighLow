using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighLow
{
    public class Game
    {
        #region private variables
        //Max number for the game
        private readonly int _high,
                             //Min number for the game
                             _low,
                             //The randomly generated number
                             _answer;
        //How many rounds have past
        private int _rounds = 0;

        //A list of tuples for previous guesses, 
        //Item1 - previous guess 
        //Item2 - true if guess was higher than answer 
        private List<Tuple<int, bool>> _previousGuesses = new List<Tuple<int, bool>>();
        #endregion

        //Set default colors to reference
        private readonly ConsoleColor _highColor = ConsoleColor.Magenta,
                                      _lowColor = ConsoleColor.Cyan,
                                      _validationColor = ConsoleColor.Red,
                                      _titleColor = ConsoleColor.White;

        #region Constructor
        public Game(int low, int high)
        {
            _low = low;
            _high = high;
            //Seed a random object
            Random rnd = new Random();
            //Answer gets a value between low and high 
            _answer = rnd.Next(low, high);
        }
        #endregion

        #region Public Methods

        public void Play()
        {
            //Will never be equal to guess since always below smallest value
            int guess = _low - 1;
            //Repeat as long as the guess is not the answer
            while (guess != _answer)
            {
                //Get input and parse/validate repeat as long as the input is not valid
                guess = _GetGuess();

                //Check if the guess is to high to low or correct
                _CheckGuess(guess);
                
                //Increment the round number by 1
                _rounds++;
            }
            //Congradulate the user
            Console.WriteLine("Congradulations it took you {0} tries!", _rounds);
        }

        public void Title()
        {
            //Displays instructions
            Console.ForegroundColor = _titleColor;
            Console.Write("Welcome to ");
            Console.ForegroundColor = _highColor;
            Console.Write("High");
            Console.ForegroundColor = _titleColor;
            Console.Write("/");
            Console.ForegroundColor = _lowColor;
            Console.Write("Low\n");
            Console.ResetColor();
            Console.WriteLine("In this game you will guess a number between {0} and {1}", _low, _high);
            Console.WriteLine("Each round you will be told if your guess is too high or too low.");
            Console.WriteLine("Try to guesss the correct answer in as few tries as possible!");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        public bool ConfirmAgain()
        {
            bool validInput;
            bool playAgain = true;
            //Check if the user wants to play again
            do
            {
                //Assume input is valid
                validInput = true;
                Console.ResetColor();
                Console.WriteLine("Would you like to play again? (Y/N)");
                //Get the raw input
                string input = Console.ReadLine();
                Console.Clear();
                //Check if the user typed "Y" or "y" if so set play to true
                //This shouldn't change "play"s value but is here to be safe
                if (input == "Y" || input == "y")
                {
                    playAgain = true;
                }
                //Check if the user typed "N" or "n" if so set play to false
                //This will set the game loop to end
                else if (input == "N" || input == "n")
                {
                    playAgain = false;
                }
                //If neither "Y/y" or "N/n" are typed inform user of an invalid input
                //Set validInput to false to repeat asking the user if they would like
                //to play.
                else
                {
                    Console.ForegroundColor = _validationColor;
                    validInput = false;
                    Console.WriteLine("Invalid input {0}", input);
                }
            } while (!validInput);

            return playAgain;
        }

        private int _GetGuess()
        {
            int guess;
            //Assume input is valid
            bool validInput = true;
            //Get input and parse/validate repeat as long as the input is not valid
            do
            {
                _ListPreviousGuess();
                //Ask for input
                Console.ResetColor();
                Console.WriteLine("Enter a number between {0} and {1}:", _low, _high);

                //Get the raw input
                string input = Console.ReadLine();
                Console.Clear();
                //Attempt to parse the input (if successful enter the if statement)
                if (int.TryParse(input, out guess))
                {
                    Console.ForegroundColor = _validationColor;
                    //If the input is to small set valid to false
                    if (guess < 1)
                    {
                        Console.WriteLine("Your number must be at least {0}.", _low);
                        validInput = false;
                    }
                    //If the input is to large set valid to false
                    else if (guess > 100)
                    {
                        Console.WriteLine("Your number should be at most {0}.", _high);
                        validInput = false;
                    }
                }
                //If you cannot parse the input set valid to false
                else
                {
                    Console.ForegroundColor = _validationColor;
                    //To type " use the escape key \
                    Console.WriteLine("\"{0}\" is not a valid number.", input);
                    validInput = false;
                }

            } while (!validInput);
            return guess;
        }

        public void _ListPreviousGuess()
        {
            if (_previousGuesses.Count > 0)
            {
                Console.ForegroundColor = _titleColor;
                Console.Write("Previous Guesses: ");
                //Loop through each previous guess
                foreach (Tuple<int, bool> pastGuess in _previousGuesses)
                {
                    //If the guess was higher set text to magenta
                    if (pastGuess.Item2)
                    {
                        Console.ForegroundColor = _highColor;
                    }
                    //If the guess was not higher (ie lower) set text to cyan
                    else
                    {
                        Console.ForegroundColor = _lowColor;
                    }
                    //print the guess
                    Console.Write("{0} ", pastGuess.Item1);
                }
                //Move to next line
                Console.Write("\n");
            }
        }
        #endregion

        #region Private Methods

        private void _CheckGuess(int guess)
        {
            //If the guess is to large
            if (guess > _answer)
            {
                //Store guess and info player
                Console.ForegroundColor = _highColor;
                _previousGuesses.Add(new Tuple<int, bool>(guess, true));
                Console.WriteLine("Your guess is to high!");
            }
            //if the guess is to small
            else if (guess < _answer)
            {
                //Store guess and info player
                Console.ForegroundColor = _lowColor;
                _previousGuesses.Add(new Tuple<int, bool>(guess, false));
                Console.WriteLine("Your guess is to low!");
            }
            //if the guess is correct 
            else
            {
                //Inform player they are correct
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You guess correct!");
            }
        }
        #endregion
    }
}
