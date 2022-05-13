using API_Portafolio.BL.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API_Portafolio.Management
{
    public class JWT
    {
        public string create_token(JWTRequest jwtr)
        {
            jwtr.FechaIngreso = DateTime.UtcNow.ToString("MM-dd-yyyy");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("secretsecretsecret");
            var tokenDescriptor = SecurityAlg(jwtr, key);

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public JWTRequest validate_token(JWTRequest jwtr)
        {
            JWTRequest jwtToken = new JWTRequest();
            if (jwtr.token == null)
            {
                return jwtToken;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("secretsecretsecret");

            try
            {
                tokenHandler.ValidateToken(jwtr.token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                var jwtTokenV = (JwtSecurityToken)validatedToken;
                jwtToken.user = jwtTokenV.Claims.First(x => x.Type == "user").Value.ToString();
                jwtToken.IP = jwtTokenV.Claims.First(x => x.Type == "IP").Value.ToString();
                jwtToken.loc = jwtTokenV.Claims.First(x => x.Type == "loc").Value.ToString();
                jwtToken.FechaIngreso = jwtTokenV.Claims.First(x => x.Type == "fechaIngreso").Value.ToString();
                jwtToken.alg = jwtTokenV.Claims.First(x => x.Type == "alg").Value.ToString();

                return jwtToken;

            }
            catch (SecurityTokenException)
            {
                return jwtToken;
            }
            catch (Exception ex)
            {
                return jwtToken;
            }


        }

        public SecurityTokenDescriptor SecurityAlg(JWTRequest jwtr, byte[] key)
        {
            SecurityTokenDescriptor tokenDescriptor;
            if (jwtr.alg == "HS256")
            {
                tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new[]
                {
                    new Claim("user", jwtr.user),
                    new Claim("fechaIngreso", jwtr.FechaIngreso),
                    new Claim("IP", jwtr.IP),
                    new Claim("loc", jwtr.loc),
                    new Claim("alg", jwtr.alg)
                }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
            }
            else if (jwtr.alg == "HS384")
            {
                tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new[]
                {
                    new Claim("user", jwtr.user),
                    new Claim("fechaIngreso", jwtr.FechaIngreso),
                    new Claim("IP", jwtr.IP),
                    new Claim("loc", jwtr.loc)
                }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
            }
            else
            {
                tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new[]
                {
                    new Claim("user", jwtr.user),
                    new Claim("fechaIngreso", jwtr.FechaIngreso),
                    new Claim("IP", jwtr.IP),
                    new Claim("loc", jwtr.loc)
                }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
            }
            return tokenDescriptor;
        }
    }
}
