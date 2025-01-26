using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DemoResponse : ResponseBase
    {
        public List<Demo> DemoRecords { get; set; }

        public DemoResponse()
        {
            DemoRecords = new List<Demo>();
        }
    }
}