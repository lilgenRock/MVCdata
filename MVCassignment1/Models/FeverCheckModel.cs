using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCassignment1.Models
{
    public class FeverCheckModel
    {
        static public bool CheckInput(string inputTemp)
        {
            return float.TryParse(inputTemp, out float test);
        }

        static public float CalcFahrenheit(float tempC)
        {
            return (9F / 5F * tempC) + 32;
        }

        static public float CalcCelsius(string inputTemp, string scale)
        {
            if (scale == "Fahrenheit")
            {
                return (float.Parse(inputTemp) - 32) * 5 / 9;
            }
            else
            {
                return float.Parse(inputTemp);
            }
        }

        static public string GetMessage(float tempC)
        {
            if (tempC > 37.2)
            {
                return  "You are hot hot hot!";
            }
            else if (tempC < 10.0)
            {
                return "You are a vampire!";
            }
            else if (tempC < 36.1)
            {
                return "You are cool!";
            }
            else
            {
                return "You are fine!";
            }
        }
    }
}