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
        public const string SNOWMAN_2 = " *   _ *   ";
        public const string SNOWMAN_3 = "   _[_]_ * ";
        public const string SNOWMAN_4 = " *  (\")    ";
        public const string SNOWMAN_5 = "  \\( : )/ *";
        public const string SNOWMAN_6 = "* (_ : _)  ";
        public const string SNOWMAN_7 = "-----------";

        public const int SNOWMAN_MIN_WORD_LENGTH = 5;
        public const int SNOWMAN_MAX_WORD_LENGTH = 8;
        public const int SNOWMAN_MAX_WRONG_GUESSES = 7;

        public static void Start()
        {    
            // Console.WriteLine("Please enter p to play or t to test => ");
            Console.WriteLine("Please enter p to play: ");
            string userInput = Console.ReadLine().ToLower();

            if(userInput== "p")
            {
                PlaySnowmanGame();
            }
            // else if(userInput == "t")
            // {
            //     Console.WriteLine("This should be testing....");
            //     SnowmanTests.PrintsSuccessMessageIfAllLettersGuessed();

            // }
            // else
            // {
            //     Console.WriteLine("Invalid input. Please enter either p or t."); 
            // }
            
        }

        public static void PlaySnowmanGame()
        /*
        Method to handle the main game logic
        */
        {
            WordGenerator myWordGenerator = new WordGenerator();
            string selectedWord = SelectWord(myWordGenerator);
            Snowman(selectedWord);
        }

        public static string SelectWord(WordGenerator wordGenerator)
        /*
        Method to select a word within specified length constraints
        */
        {
            string selectedWord;

            do
            {
                selectedWord = wordGenerator.GetWord(PartOfSpeech.noun);
            } while (selectedWord.Length < SNOWMAN_MIN_WORD_LENGTH || selectedWord.Length > SNOWMAN_MAX_WORD_LENGTH);

            return selectedWord;
        }

        public static void Snowman(string snowmanWord)
        {
            /*
            Complete the snowman function
            replace "pass" below with your own code
            It should print 'Congratulations, you win!'
            If the player wins and, 'Sorry, you lose! The word was {snowmanWord}' if the player loses
            */
            Console.WriteLine("\nDebug info: "+ snowmanWord);

            Dictionary<char, bool> snowmanWordDict = BuildWordDictionary(snowmanWord);
            List<char> wrongGuessesList = new List<char>();
      
            bool continueGuess = true;

            while(wrongGuessesList.Count<SNOWMAN_MAX_WRONG_GUESSES && continueGuess)
            {
               char userInputLetter = GetLetterFromUser(snowmanWordDict, wrongGuessesList);
               
                if (snowmanWord.Contains(userInputLetter))
                {
                    snowmanWordDict[userInputLetter]=true;
                    Console.WriteLine("You guessed a letter that's in the word!");
                }
                else {
                    wrongGuessesList.Add(userInputLetter);
                    Console.WriteLine("The letter is not in the word");
                }

                int numWrongGuesses = wrongGuessesList.Count;
                PrintSnowmanGraphic(numWrongGuesses);
                PrintWordProgressString(snowmanWord, snowmanWordDict);
                Console.WriteLine($"\nWrong guesses: {string.Concat(wrongGuessesList)}\n");

                if (GetWordProgress(snowmanWord, snowmanWordDict))
                {
                    Console.WriteLine("Congratulations, you win!");
                    continueGuess = false;
                }
                else if(wrongGuessesList.Count==SNOWMAN_MAX_WRONG_GUESSES)
                {
                    Console.WriteLine($"Sorry, you lose! The word was " + snowmanWord);
                }
            }
        }

        public static void PrintSnowmanGraphic(int numWrongGuesses)
        {
            /*
            This function prints out the appropriate snowman image 
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

        public static char GetLetterFromUser(Dictionary<char, bool> snowmanWordDict, List<char> wrongGuessesList)
        {
            
            /*
            This function takes the snowmanWordDict and the list of characters 
            that have been guessed incorrectly (wrongGuessesList) as input.
            It asks for input from the user of a single character until 
            a valid character is provided and then returns this character.
            */

            bool validInput = false;
            char letter = '\0';
            // char? letter = null;

            while (!validInput)
            {
                Console.WriteLine("Guess a letter: ");
                string userInputString = Console.ReadLine().ToLower();

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

        public static Dictionary<char, bool> BuildWordDictionary(string snowmanWord)
        {
            /*
            This function takes snowmanWord as input and returns 
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

        public static void PrintDictionary(Dictionary<char, bool> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                Console.WriteLine($"Key:{kvp.Key}, Value: {kvp.Value}");
            }
        }

        public static void PrintWordProgressString(string snowmanWord, Dictionary<char, bool> snowmanWordDict)
        {
            /*
            This function takes the snowmanWord and snowmanWordDict as input.
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

        public static bool GetWordProgress(string snowmanWord, Dictionary<char, bool> snowmanWordDictionary)
        {
            /*
            This function takes the snowmanWord and snowmanWordDict as input.
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




