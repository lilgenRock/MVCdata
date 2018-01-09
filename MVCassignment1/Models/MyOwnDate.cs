using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCassignment1.Models
{
    public class MyOwnDate
    {
        public static string Date { get; set; }

        static MyOwnDate()
        {
            Date = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}

