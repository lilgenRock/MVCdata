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
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult GuessingGame(string inputGuess)
        {
            string guessStr = GuessingGameModel.EvaluateGuess(Int32.Parse(inputGuess), (int)Session["randomNumber"]);

            Session["guessingCounter"] =  (int)Session["guessingCounter"] + 1;
            Session["guessingList"] += inputGuess+" ";
            ViewBag.msg = "<h4>Guessing history: "+ Session["guessingList"] + "</h4><h4>Your guess " + inputGuess + " was " + guessStr + ". Counter: " + Session["guessingCounter"] + ". Please try again!</h4>";
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