using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Portafolio.BL.Models
{
    public class Request
    {
        public int status { get; set; }
        public string message { get; set; }
        public Object data { get; set; }

    }
}
