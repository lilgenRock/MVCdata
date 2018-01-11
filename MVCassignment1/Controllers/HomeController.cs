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
            inputValueOk = float.TryParse(inputTemp, out float test);
            if (inputValueOk)
            {
                if (scale == "Fahrenheit")
                {
                    tempC = (float.Parse(inputTemp) - 32) * 5 / 9;
                    ViewBag.checkedF = "checked";
                }
                else
                {
                    tempC = float.Parse(inputTemp);
                    ViewBag.checkedC = "checked";
                }

                if (tempC > 37.2)
                {
                    msg = "You are hot hot hot!";
                }
                else if (tempC < 36.1)
                {
                    msg = "You are cool!";
                }else
                {
                    msg = "You are fine!";
                }
                tempF = (9F / 5F * tempC) + 32;
                ViewBag.msgC = "Your temp is " + tempC.ToString().Replace(',', '.') + " Celsius. " + msg;
                ViewBag.msgF = "Your temp is " + tempF.ToString().Replace(',', '.') + " Fahrenheit. " + msg;
            }
            return View();
        }
    }
}