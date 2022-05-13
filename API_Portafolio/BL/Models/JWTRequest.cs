using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Portafolio.BL.Models
{
    public class JWTRequest
    {
        public string user { get; set; }
        public string alg { get; set; }
        public string token { get; set; }
        public string IP { get; set; }
        public string loc { get; set; }
        public string FechaIngreso { get; set; }

        public JWTRequest(string User, string Ip, string Alg, string Loc, string fechaingreso)
        {
            user = User;
            alg = Alg;
            IP = Ip;
            loc = Loc;
            FechaIngreso = fechaingreso;
        }

        public JWTRequest(string Token)
        {
            token = Token;
        }
        public JWTRequest(string User, string Alg, string Token, string Ip, string Loc, string fechaingreso)
        {
            user = User;
            alg = Alg;
            token = Token;
            IP = Ip;
            loc = Loc;
            FechaIngreso = fechaingreso;
        }
        public JWTRequest()
        {
            
        }

    }
}
