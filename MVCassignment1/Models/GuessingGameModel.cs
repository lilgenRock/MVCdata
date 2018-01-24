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
            string separator = "";
            bool newScoreInserted = false;
            string[] arrHighScores = cookie.Split('|');
            foreach (string element in arrHighScores)
            {
              if (newScoreInserted == false && Int32.Parse(element.Substring(0, element.IndexOf('='))) >= Int32.Parse(score)) // if correct position found for new score, then insert it in cookie string
                {
                    result += separator + score + "=" + name;    // separator must be "" here in the first loop or else there will be trouble
                    newScoreInserted = true;
                    separator = "|";
                }
                result += separator + element;
                separator = "|";
            }
            if (!newScoreInserted)      // if you have the worst score(highest) then add it here after the loop is done
            {
                result += separator + score + "=" + name; 
            }

            return result;
        }
    }
}