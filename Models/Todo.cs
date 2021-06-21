using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoWebApi.Models
{
    public class Todo
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public bool Done { get; set; }
    }
}