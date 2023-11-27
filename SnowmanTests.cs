using System;
using System.IO;
using NUnit.Framework;
using Moq;

namespace cSharpSnowman.tests
{
    [TestFixture]
    public class SnowmanTests
    {
        [Test]
        public void PrintsSuccessMessageIfAllLettersGuessed()
        {
            // Arrange
            string[] inputLetters = { "n", "a", "m", "w", "o", "n", "s" };
            var mockInput = new Mock<TextReader>();

          
            mockInput.SetupSequence(x => x.ReadLine())
                .Returns("n").Returns("a").Returns("m")
                .Returns("w").Returns("o").Returns("n")
                .Returns("s").Returns((string)null);

            using (var consoleOutput = new StringWriter())
            {
                Console.SetOut(consoleOutput);

                // Act
                using (var consoleInput = new StringReader(string.Join(Environment.NewLine, inputLetters)))
                {
                    Console.SetIn(consoleInput);

                    // Call the static method using the class name
                    SnowmanGame.Start();
                }

                // Assert
                StringAssert.Contains("you win", consoleOutput.ToString().ToLower());
            }
        }
    }
}


        //[Test]
        // public void PrintsSuccessMessageWith3WrongGuessesAndTheRestRight()
        // {
        //     // Arrange
        //     string[] inputLetters = { "s", "n", "b", "o", "w", "m", "a", "q", "v", "n" };
        //     var mockInput = new Mock<TextReader>();
        //     mockInput.SetupSequence(x => x.ReadLine())
        //         .Returns("s").Returns("n").Returns("b")
        //         .Returns("o").Returns("w").Returns("m")
        //         .Returns("a").Returns("q").Returns("v")
        //         .Returns("n").Returns(null);

        //     using (var consoleOutput = new StringWriter())
        //     {
        //         Console.SetOut(consoleOutput);

        //         // Act
        //         using (var consoleInput = new StringReader(string.Join(Environment.NewLine, inputLetters)))
        //         {
        //             Console.SetIn(consoleInput);
        //             Snowman();
        //         }

        //         // Assert
        //         StringAssert.Contains("you win", consoleOutput.ToString().ToLower());
        //         StringAssert.DoesNotContain("sorry, you lose!", consoleOutput.ToString().ToLower());
        //         StringAssert.DoesNotContain("the word was snowman", consoleOutput.ToString().ToLower());
        //     }
        // }

        // [Test]
        // public void PrintsFailureMessageWith7StraightWrongGuesses()
        // {
        //     // Arrange
        //     string[] inputLetters = { "b", "c", "p", "z", "q", "v", "x" };
        //     var mockInput = new Mock<TextReader>();
        //     mockInput.SetupSequence(x => x.ReadLine())
        //         .Returns("b").Returns("c").Returns("p")
        //         .Returns("z").Returns("q").Returns("v")
        //         .Returns("x").Returns(null);

        //     using (var consoleOutput = new StringWriter())
        //     {
        //         Console.SetOut(consoleOutput);

        //         // Act
        //         using (var consoleInput = new StringReader(string.Join(Environment.NewLine, inputLetters)))
        //         {
        //             Console.SetIn(consoleInput);
        //             Snowman();
        //         }

        //         // Assert
        //         StringAssert.DoesNotContain("you win", consoleOutput.ToString().ToLower());
        //         StringAssert.Contains("sorry, you lose!", consoleOutput.ToString().ToLower());
        //         StringAssert.Contains("the word was snowman", consoleOutput.ToString().ToLower());
        //     }
        // }

        // [Test]
        // public void PrintsFailureMessageWith7WrongGuessesAndTwoRight()
        // {
        //     // Arrange
        //     string[] inputLetters = { "s", "b", "c", "p", "n", "z", "q", "v", "x" };
        //     var mockInput = new Mock<TextReader>();
        //     mockInput.SetupSequence(x => x.ReadLine())
        //         .Returns("s").Returns("b").Returns("c")
        //         .Returns("p").Returns("n").Returns("z")
        //         .Returns("q").Returns("v").Returns("x")
        //         .Returns(null);

        //     using (var consoleOutput = new StringWriter())
        //     {
        //         Console.SetOut(consoleOutput);

        //         // Act
        //         using (var consoleInput = new StringReader(string.Join(Environment.NewLine, inputLetters)))
        //         {
        //             Console.SetIn(consoleInput);
        //             Snowman();
        //         }

        //         // Assert
        //         StringAssert.DoesNotContain("you win", consoleOutput.ToString().ToLower());
        //         StringAssert.Contains("sorry, you lose!", consoleOutput.ToString().ToLower());
        //         StringAssert.Contains("the word was snowman", consoleOutput.ToString().ToLower());
        //     }
        // }
    // }
// }
