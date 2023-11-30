// See https://aka.ms/new-console-template for more information
using System;
using System.Diagnostics.Tracing;
using CrypticWizard.RandomWordGenerator;
using static CrypticWizard.RandomWordGenerator.WordGenerator;

namespace cSharpSnowman
{
    public class SnowmanGame

    {
        public const string SNOWMAN_1 = "*   *   *  ";
        const string SNOWMAN_2 = " *   _ *   ";
        const string SNOWMAN_3 = "   _[_]_ * ";
        const string SNOWMAN_4 = " *  (\")    ";
        const string SNOWMAN_5 = "  \\( : )/ *";
        const string SNOWMAN_6 = "* (_ : _)  ";
        const string SNOWMAN_7 = "-----------";

        const int SNOWMAN_MIN_WORD_LENGTH = 5;
        const int SNOWMAN_MAX_WORD_LENGTH = 8;
        const int SNOWMAN_MAX_WRONG_GUESSES = 7;

        public static void Start()
        {
            /*
            This method repeatedly prompts the user to enter either 'p' to play the Snowman game 
            or 'q' to quit. It processes the user input, initiating the game, 
            displaying a farewell message, or requesting valid input.
            The loop continues until the user decides to play or exit.
            */

            while (true)
            {
                Console.WriteLine("Please enter 'p' to play or 'q' to quit: ");
                string? userInput = Console.ReadLine()!.ToLower();

                if (userInput == "p")
                {
                    PlaySnowmanGame();

                }
                else if (userInput == "q")
                {
                    Console.WriteLine("Exiting the program. Goodbye!");
                    break;
                }
                else
                {
                    Console.WriteLine("Input is invalid. Try again, please.");
                }
            }
        }


        static void PlaySnowmanGame()
        {
            /* 
            This method initiates an instance of class WordGenerator, 
            calls SelectWord mwthod to get a random word, and 
            calls Snowman method to handle the main game logic.
            */

            WordGenerator myWordGenerator = new WordGenerator();
            string selectedWord = SelectWord(myWordGenerator);
            Snowman(selectedWord);
        }

        static string SelectWord(WordGenerator myWordGenerator)
        {
            /* 
            This method generates a new selected word until it will satisfied specific length constraints.
             */

            string selectedWord;

            do
            {
                selectedWord = myWordGenerator.GetWord(PartOfSpeech.noun);
            }
            while (selectedWord.Length < SNOWMAN_MIN_WORD_LENGTH || selectedWord.Length > SNOWMAN_MAX_WORD_LENGTH);

            return selectedWord;
        }

        static void Snowman(string snowmanWord)
        {
            /*
            This method contains main game logic.
            It will print 'Congratulations, you win!', if the player wins
            and, 'Sorry, you lose! The word was {snowmanWord}' if the player loses.
            */

            //Use for debuging
            //Console.WriteLine("\nDebug info: "+ snowmanWord);

            Dictionary<char, bool> snowmanWordDict = BuildWordDictionary(snowmanWord);
            var wrongGuessesList = new List<char>();

            bool continueGuess = true;

            while (wrongGuessesList.Count < SNOWMAN_MAX_WRONG_GUESSES && continueGuess)
            {
                char userInputLetter = GetLetterFromUser(snowmanWordDict, wrongGuessesList);

                if (snowmanWord.Contains(userInputLetter))
                {
                    snowmanWordDict[userInputLetter] = true;
                    Console.WriteLine("You guessed a letter that's in the word!");
                }
                else
                {
                    wrongGuessesList.Add(userInputLetter);
                    Console.WriteLine("The letter is not in the word");
                }

                int numWrongGuesses = wrongGuessesList.Count;
                PrintSnowmanGraphic(numWrongGuesses);
                PrintWordProgressString(snowmanWord, snowmanWordDict);
                Console.WriteLine($"\nWrong guesses: {string.Concat(wrongGuessesList)}\n");

                if (GetWordProgress(snowmanWord, snowmanWordDict))
                {
                    Console.WriteLine("Congratulations, you win!\n");
                    continueGuess = false;
                }
                else if (wrongGuessesList.Count == SNOWMAN_MAX_WRONG_GUESSES)
                {
                    Console.WriteLine($"Sorry, you lose! The word was " + snowmanWord + "\n");
                }
            }
        }

        static void PrintSnowmanGraphic(int numWrongGuesses)
        {
            /*
            This method prints out the appropriate snowman image 
            depending on the number of wrong guesses the player has made.
            */

            for (int i = 1; i < numWrongGuesses + 1; i++)
            {
                if (i == 1)
                {
                    Console.WriteLine(SNOWMAN_1);
                }
                else if (i == 2)
                {
                    Console.WriteLine(SNOWMAN_2);
                }
                else if (i == 3)
                {
                    Console.WriteLine(SNOWMAN_3);
                }
                else if (i == 4)
                {
                    Console.WriteLine(SNOWMAN_4);
                }
                else if (i == 5)
                {
                    Console.WriteLine(SNOWMAN_5);
                }
                else if (i == 6)
                {
                    Console.WriteLine(SNOWMAN_6);
                }
                else if (i == 7)
                {
                    Console.WriteLine(SNOWMAN_7);
                }
            }
        }

        static char GetLetterFromUser(Dictionary<char, bool> snowmanWordDict, List<char> wrongGuessesList)
        {
            /*
            This method takes the snowmanWordDict and the list of characters 
            that have been guessed incorrectly (wrongGuessesList) as input.
            It asks for input from the user of a single character until 
            a valid character is provided and then returns this character.
            */

            bool validInput = false;
            char letter = '\0';

            while (!validInput)
            {
                Console.WriteLine("Guess a letter or press Ctrl + C to exit the game: ");
                string? userInputString = Console.ReadLine()!.ToLower();

                if (userInputString.Length != 1)
                {
                    Console.WriteLine("You can only input one letter at a time!");
                }
                else
                {
                    letter = userInputString[0];

                    if (!Char.IsLetter(letter))
                    {
                        Console.WriteLine("You must input a letter!");
                    }
                    else if (snowmanWordDict.ContainsKey(letter) && snowmanWordDict[letter])
                    {
                        Console.WriteLine("You already guessed that letter and it's in the word!");
                    }
                    else if (wrongGuessesList.Contains(letter))
                    {
                        Console.WriteLine("You already guessed that letter and it's not in the word!");
                    }
                    else
                    {
                        validInput = true;
                    }
                }
            }

            Console.WriteLine($"User input: {letter}");
            return letter;
        }

        static Dictionary<char, bool> BuildWordDictionary(string snowmanWord)
        {
            /*
            This method takes snowmanWord as input and returns 
            a dictionary with a key-value pair for each letter in 
            snowmanWord where the key is the letter and the value is `false`.
            */

            Dictionary<char, bool> snowmanWordDictionary = new Dictionary<char, bool>();

            foreach (char letter in snowmanWord)
            {
                snowmanWordDictionary[letter] = false;
            }
            return snowmanWordDictionary;
        }

        static void PrintDictionary(Dictionary<char, bool> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                Console.WriteLine($"Key:{kvp.Key}, Value: {kvp.Value}");
            }
        }

        static void PrintWordProgressString(string snowmanWord, Dictionary<char, bool> snowmanWordDict)
        {
            /*
            This method takes the snowmanWord and snowmanWordDict as input.
            It prints an output_string that shows the correct letter guess placements 
            as well as the placements for the letters yet to be guessed.
            */

            string outputString = "";

            foreach (char letter in snowmanWord)
            {
                outputString += snowmanWordDict[letter] ? letter : '_';
                outputString += " ";
            }
            Console.WriteLine(outputString);
        }

        static bool GetWordProgress(string snowmanWord, Dictionary<char, bool> snowmanWordDictionary)
        {
            /*
            This method takes the snowmanWord and snowmanWordDict as input.
            It returns True if all the letters of the word have been guessed, and false otherwise.
            */

            foreach (char letter in snowmanWord)
            {
                if (snowmanWordDictionary[letter] == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}




