using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MVCassignment1.Models;

namespace MVCassignment1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home
        public ActionResult About()
        {
            return View();
        }

        // GET: Projects
        public ActionResult Projects()
        {
            return View();
        }

        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }

        // GET: GuessingGame
        [HttpGet]
        public ActionResult GuessingGame()
        {
            Random rnd = new Random();
            GuessingGameModel model = new GuessingGameModel();
            if (Session["randomNumber"] == null || Session["guessingCounter"] == null)
            {
                Session["randomNumber"] = rnd.Next(1, 101);
                Session["guessingCounter"] = 0;
                Session["guessingList"] = "";
                ViewBag.name = "Enter your name here";
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult GuessingGame(string inputGuess, string inputName)
        {
            string guessStr = GuessingGameModel.EvaluateGuess(Int32.Parse(inputGuess), (int)Session["randomNumber"]);
            Session["guessingCounter"] =  (int)Session["guessingCounter"] + 1;
            Session["guessingList"] += inputGuess+" ";
            if (inputName != null && inputName.Length > 0)
            {
                Session["guessingName"] = inputName;
            }
            ViewBag.msg = "<h5>"+ Session["guessingName"] + "'s guessing history: " + Session["guessingList"] + " (Guesses: " + Session["guessingCounter"] + ")</h5><h5>Your guess " + inputGuess + " was " + guessStr + "</h5>";
            if (Int32.Parse(inputGuess) == (int)Session["randomNumber"])    // if player guessed correctly
            {
                Random rnd = new Random();
                ViewBag.msg += "<h5>Well done!</h5><h5>Can you beat that score? A new number has been generated for you to guess.</h5>";
                ViewBag.name = Session["guessingName"];
                if (Request.Cookies["HighScore"] == null)   // if this is the first score, then highscore cookie is empty so we create one.
                {
                    HttpCookie HighScore = new HttpCookie("HighScore");
                    HighScore["hs"] = Session["guessingCounter"] + "=" + Session["guessingName"];
                    Response.Cookies.Add(HighScore);
                }
                else                                        // highscore cookie exists and we add a new score in the right position in the string
                {
                    HttpCookie HighScore = Request.Cookies["HighScore"];
                    HighScore["hs"] = GuessingGameModel.SortAndInsertHighScore(HighScore["hs"], ""+Session["guessingCounter"], ""+Session["guessingName"]);
                    Response.Cookies.Add(HighScore);
                }
                ViewBag.HighScore = GuessingGameModel.FormatHighScoreList(Request.Cookies["HighScore"]["hs"]);
                Session["randomNumber"] = rnd.Next(1, 101); // This is DRY. How can I do a session-starter-function?
                Session["guessingCounter"] = 0;
                Session["guessingList"] = "";

            }
            else                                    // incorrect guess
            {
                ViewBag.msg += "<h5>Please try again!</h5>";
            }
            return View();
        }

        // GET: FeverCheck
        [HttpGet]
        public ActionResult FeverCheck()
        {
            FeverCheckModel model = new FeverCheckModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult FeverCheck(string inputTemp, string scale)
        {
            string msg = "Please input numeric value only.";
            float tempC = 0;
            float tempF = 0;
            bool inputValueOk = false;
            ViewBag.checkedC = "";
            ViewBag.checkedF = "";
            ViewBag.msgC = "";
            ViewBag.msgF = "";
            inputTemp = inputTemp.Replace('.',',');

            inputValueOk = FeverCheckModel.CheckInput(inputTemp);
            if (inputValueOk)   // if input ok then calc stuff and set message
            {
                tempC = FeverCheckModel.CalcCelsius(inputTemp, scale);
                tempF = FeverCheckModel.CalcFahrenheit(tempC);
                msg   = FeverCheckModel.GetMessage(tempC);
                ViewBag.msgC = "Your temp is " + tempC.ToString().Replace(',', '.') + " Celsius. " + msg;
                ViewBag.msgF = "Your temp is " + tempF.ToString().Replace(',', '.') + " Fahrenheit. " + msg;
                if(scale == "Fahrenheit")
                {
                    ViewBag.checkedF = "checked";
                }
                else
                {
                    ViewBag.checkedC = "checked";
                }
            }
            else            // if input NOT ok then set error msg
            {
                ViewBag.msg = msg;
            }
            return View();
        }
        
    }
}