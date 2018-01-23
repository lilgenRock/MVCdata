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
                return "too high!";
            }
            else if (guessNumber < correctNumber)
            {
                return "too low!";
            }
            else
            {
                return "correct!";
            }
        }
        public static string FormatHighScoreList(string cookie)
        {
            string result="<h3>HighScores</h3>";
            int position = 0;
            string[] arrTemp = cookie.Split('|'); 
            foreach(string element in arrTemp)
            {
                position++;
                result += "<h4>" + position + ". " + element +"</h4>";
            }
            return result;
        }

        public static string SortAndInsertHighScore(string cookie, string score, string name)
        {
            string result = "";
            string[] arrHighScores = cookie.Split('|');

            return result;
        }
/*
        public static void StartNewGuessingGame()
        {
            Random rnd = new Random();
            Session["randomNumber"] = rnd.Next(1, 101);
            Session["guessingCounter"] = 0;
            Session["guessingList"] = "";
        }
        */
    }
}