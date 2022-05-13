using API_Portafolio.BL.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Portafolio.BL.DAO
{
    public interface JWTDAO
    {
        JWTRequest create_token(JWTRequest jwtr);
        JWTRequest validate_token(JWTRequest jwtr);
        SecurityTokenDescriptor SecurityAlg(JWTRequest jwtr, byte[] key);
    }
}
