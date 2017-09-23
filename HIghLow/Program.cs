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
            //Max number for the game
            int high = 100,
                //Min number for the game
                low = 1,
                //The current guess
                guess = 0,
                //The randomly generated number
                answer,
                //How many rounds have past
                rounds;

            //A list of tuples for previous guesses, 
            //Item1 - previous guess 
            //Item2 - true if guess was higher than answer 
            List<Tuple<int,bool>> previousGuesses = new List<Tuple<int,bool>>();
            //Raw user input from the console
            string input;
            //If the input is valid
            bool validInput = true,
                //If the game should continue
                 play = true;

            //Set default colors to reference
            ConsoleColor highColor = ConsoleColor.Magenta,
                         lowColor = ConsoleColor.Cyan,
                         validationColor = ConsoleColor.Red,
                         titleColor = ConsoleColor.White;

            //Displays instructions
            Console.ForegroundColor = titleColor;
            Console.Write("Welcome to ");
            Console.ForegroundColor = highColor;
            Console.Write("High");
            Console.ForegroundColor = titleColor;
            Console.Write("/");
            Console.ForegroundColor = lowColor;
            Console.Write("Low\n");
            Console.ResetColor();
            Console.WriteLine("In this game you will guess a number between {0} and {1}", low, high);
            Console.WriteLine("Each round you will be told if your guess is too high or too low.");
            Console.WriteLine("Try to guesss the correct answer in as few tries as possible!");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();

            //Game loop
            while (play)
            {
                //Seed a random object
                Random rnd = new Random();
                //Answer gets a value between low and high 
                answer = rnd.Next(low, high);
                //Set round count to zero
                rounds = 0;
                //Clear previous guess in case this is a second play through
                previousGuesses.Clear();

                //Repeat as long as the guess is not the answer
                while (guess != answer)
                {
                    //Get input and parse/validate repeat as long as the input is not valid
                    do
                    {
                        //Assume input is valid
                        validInput = true;
                        //If there are any past guesses
                        if (previousGuesses.Count > 0)
                        {
                            Console.ForegroundColor = titleColor;
                            Console.Write("Previous Guesses: ");
                            //Loop through each previous guess
                            foreach (Tuple<int, bool> pastGuess in previousGuesses)
                            {
                                //If the guess was higher set text to magenta
                                if (pastGuess.Item2)
                                {
                                    Console.ForegroundColor = highColor;
                                }
                                //If the guess was not higher (ie lower) set text to cyan
                                else
                                {
                                    Console.ForegroundColor = lowColor;
                                }
                                //print the guess
                                Console.Write("{0} ", pastGuess.Item1);
                            }
                            //Move to next line
                            Console.Write("\n");
                        }
                        //Ask for input
                        Console.ResetColor();
                        Console.WriteLine("Enter a number between {0} and {1}:", low, high);

                        //Get the raw input
                        input = Console.ReadLine();
                        Console.Clear();
                        //Attempt to parse the input (if successful enter the if statement)
                        if (int.TryParse(input, out guess))
                        {
                            Console.ForegroundColor = validationColor;
                            //If the input is to small set valid to false
                            if (guess < 1)
                            {
                                Console.WriteLine("Your number must be at least {0}.", low);
                                validInput = false;
                            }
                            //If the input is to large set valid to false
                            else if (guess > 100)
                            {
                                Console.WriteLine("Your number should be at most {0}.", high);
                                validInput = false;
                            }
                        }
                        //If you cannot parse the input set valid to false
                        else
                        {
                            Console.ForegroundColor = validationColor;
                            //To type " use the escape key \
                            Console.WriteLine("\"{0}\" is not a valid number.", input);
                            validInput = false;
                        }

                    } while (!validInput);
                    
                    //If the guess is to large
                    if (guess > answer)
                    {
                        //Store guess and info player
                        Console.ForegroundColor = highColor;
                        previousGuesses.Add(new Tuple<int, bool>(guess, true));
                        Console.WriteLine("Your guess is to high!");
                    }
                    //if the guess is to small
                    else if (guess < answer)
                    {
                        //Store guess and info player
                        Console.ForegroundColor = lowColor;
                        previousGuesses.Add(new Tuple<int, bool>(guess, false));
                        Console.WriteLine("Your guess is to low!");
                    }
                    //if the guess is correct 
                    else
                    {
                        //Inform player they are correct
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("You guess correct!");
                    }
                    //Increment the round number by 1
                    rounds++;
                }
                //Congradulate the user
                Console.WriteLine("Congradulations it took you {0} tries!", rounds);
                
                //Check if the user wants to play again
                do
                {
                    //Assume input is valid
                    validInput = true;
                    Console.ResetColor();
                    Console.WriteLine("Would you like to play again? (Y/N)");
                    //Get the raw input
                    input = Console.ReadLine();
                    Console.Clear();
                    //Check if the user typed "Y" or "y" if so set play to true
                    //This shouldn't change "play"s value but is here to be safe
                    if (input == "Y" || input == "y")
                    {
                        play = true;
                    }
                    //Check if the user typed "N" or "n" if so set play to false
                    //This will set the game loop to end
                    else if (input == "N" || input == "n")
                    {
                        play = false;
                    }
                    //If neither "Y/y" or "N/n" are typed inform user of an invalid input
                    //Set validInput to false to repeat asking the user if they would like
                    //to play.
                    else
                    {
                        Console.ForegroundColor = validationColor;
                        validInput = false;
                        Console.WriteLine("Invalid input {0}", input);
                    }
                } while (!validInput);
                
            }
            //Thank user for playing
            Console.WriteLine("Thank you for playing.");
            Console.WriteLine("Press any key to exit the game");
            Console.ReadKey();
        }
    }
}
