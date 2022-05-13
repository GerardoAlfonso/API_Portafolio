using API_Portafolio.BL.DAO;
using API_Portafolio.BL.Models;
using API_Portafolio.Management;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API_Portafolio.Controllers
{
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Route("[controller]")]
    public class JWTController : ControllerBase
    {
        private readonly JWTDAO jwtDAO;

        public JWTController(JWTDAO dao)
        {
            jwtDAO = dao;
        }

        [HttpPost]
        [Route("CreateToken")]
        public IActionResult Create_token([FromBody] JWTRequest jwtr)
        {
            Request reply = new Request();
            try
            {
                JWTRequest jwtToken = jwtDAO.create_token(jwtr);
                reply.data = jwtToken;
                reply.status = 1;
            }
            catch (Exception ex)
            {
                reply.status = 0;
                reply.message = ex.Message;
            }
            return Ok(reply);
        }

        [HttpPost]
        [Route("ValidateToken")]
        public IActionResult ValidateToken([FromBody] JWTRequest jwtr)
        {
            Request reply = new Request();
            try
            {
                JWTRequest jwtToken = jwtDAO.validate_token(jwtr);
                if(jwtToken.user != null)
                {
                    reply.data = jwtToken;
                    reply.status = 1;
                }
                else
                {
                    reply.data = jwtToken;
                    reply.status = 0;
                }
            }catch (Exception ex)
            {
                reply.status = 0; 
                reply.message = ex.Message;
            }
            return Ok(reply);
        }

    }
}
