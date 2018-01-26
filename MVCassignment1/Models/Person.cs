using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCassignment2.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }


      /*  public static bool LookForSearchString(string s)
        {
            if ((s.Contains() > 5) &&
                       (s.Substring(s.Length - 6).ToLower() == "saurus"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        */
    }
}