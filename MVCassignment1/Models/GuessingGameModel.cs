using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCassignment1.Models
{
    public class GuessingGameModel
    {
        public static string EvaluateGuess(int guessNumber, int correctNumber)
        {
            if(guessNumber > correctNumber)
            {
                return "Too high!";
            }
            else if (guessNumber < correctNumber)
            {
                return "Too low!";
            }
            else
            {
                return "Correct!";
            }
        }
    }
}