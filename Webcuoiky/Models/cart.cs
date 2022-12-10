using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webcuoiky.Models
{
    public class cart
    {
        public int pro_id { get; set; }
        public string pro_name { get; set;}
        public int price { get; set;}
        public int qty { get; set;}
        public int total { get; set;}
    }
}