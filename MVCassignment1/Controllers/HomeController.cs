using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MVCassignment1.Models;
using MVCassignment2.Models;

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

        [HttpPost]
        public ActionResult Search(string SearchString)
        {
            List<Person> people = (List<Person>)Session["people"];
            List<Person> peopleSearchResult = people.Where(p => p.Name.Contains(SearchString) || p.City.Contains(SearchString)).ToList();
            Session["peopleSearchResult"] = peopleSearchResult;
            Session["peopleSearchString"] = SearchString;
            return RedirectToAction("People", "Home");

        }


        [HttpPost]
        public ActionResult People(string Name, string PhoneNumber, string City)
        {
            List<Person> people = (List<Person>)Session["people"];
            int newId = 0;
            if (Name != "" && Name != null)
            {
                if(people.Count > 0)        // if people exist in list find the largest id and give new person that value + 1
                {
                    newId = people.Max(p => p.Id) + 1;
                }
                people.Add(new Person
                {
                    Id = newId,
                    Name = Name,
                    PhoneNumber = PhoneNumber,
                    City = City
                });
            }
            Session["people"] = people;
            return View(people);
        }

        public ActionResult SortByName()
        {
            List<Person> people = (List<Person>)Session["people"];
            if(Session["peopleNameSorted"] == null || (string)Session["peopleNameSorted"] == "desc")
            {
                people = people.OrderBy(o => o.Name).ToList();
                Session["peopleNameSorted"] = "asc" ;
            }
            else if((string)Session["peopleNameSorted"] == "asc")
            {
                people = people.OrderByDescending(o => o.Name).ToList();
                Session["peopleNameSorted"] = "desc";
            }
            Session["people"] = people;
            return RedirectToAction("People", "Home");
        }

        public ActionResult SortByCity()
        {
            List<Person> people = (List<Person>)Session["people"];
            if (Session["peopleCitySorted"] == null || (string)Session["peopleCitySorted"] == "desc")
            {
                people = people.OrderBy(o => o.City).ToList();
                Session["peopleCitySorted"] = "asc";
            }
            else if ((string)Session["peopleCitySorted"] == "asc")
            {
                people = people.OrderByDescending(o => o.City).ToList();
                Session["peopleCitySorted"] = "desc";
            }
            Session["people"] = people;
            return RedirectToAction("People", "Home");
        }

        public ActionResult DeletePerson(int id)
        {

            List<Person> people = (List<Person>)Session["people"];

            //people.RemoveAt(id);
            people = people.Where(x => x.Id != id).ToList();
            
            Session["people"] = people;

            return RedirectToAction("People", "Home");
        }


        // GET: People
        [HttpGet]
        public ActionResult People()
        {
            List<Person> people = new List<Person>();
            if (Session["people"] == null)
            {
                people.Add(new Person { Id = 0, Name = "Nisse", PhoneNumber = "0736-12345677", City = "Växjö" });
                people.Add(new Person { Id = 1, Name = "Lasse", PhoneNumber = "0708-8888888", City = "Stockholm" });
                people.Add(new Person { Id = 2, Name = "Andreas", PhoneNumber = "0736-14151617", City = "Alvesta" });
                Session["people"] = people;
            }
            else if (Session["peopleSearchResult"] != null)
            {
                people = (List<Person>)Session["peopleSearchResult"];
                ViewBag.SearchResultMessage = "Search result for: " + Session["peopleSearchString"];
                Session["peopleSearchResult"] = null;
                Session["peopleSearchString"] = null;
            }
            else
            {
                people = (List<Person>)Session["people"];
            }            
            return View(people);
        }

        [HttpPost]
        public ActionResult AddPerson(string Name, string PhoneNumber, string City)
        {

//            return RedirectToAction("People", "Home");
            return RedirectToAction("People", "Home", new { aName=Name, aPhoneNumber=PhoneNumber, aCity=City });
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