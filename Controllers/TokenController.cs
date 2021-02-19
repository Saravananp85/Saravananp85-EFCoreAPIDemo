using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using EFCoreAPIDemo.Repositories;
using EFCoreAPIDemo.Models;
using AutoMapper;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace EFCoreAPIDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IUser _userRepositories;
        private readonly IMapper _mapper;
        private IOptions<ApiBehaviorOptions> _apiBehaviorOptions;
        public TokenController(IUser userRepositories, IMapper mapper, IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            _userRepositories = userRepositories;
            _mapper = mapper;
            _apiBehaviorOptions = apiBehaviorOptions;
        }

        [HttpPost]
        public IActionResult GetToken(AuthenticationRequest authenticationRequest)
        {
            if (authenticationRequest == null)
                return BadRequest("emailId is required");

            var user = _userRepositories.Get(authenticationRequest.emailId);

            if (user != null)
            {

                string secretKey = "EFCoreAPIDemo@123";
                var issuer = "http://localhost";

                //Create security key and credential by using HmacSha256 algorithm.
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim("EmailId", user.EmailId));
                claims.Add(new Claim("FirstName", user.FirstName));
                claims.Add(new Claim("LastName", user.LastName));
                claims.Add(new Claim("RoleId", user.RoleId.ToString()));

                var token = new JwtSecurityToken(issuer,
                                        issuer,
                                        claims,
                                        expires: DateTime.Now.AddDays(1),
                                        signingCredentials: credentials);

                var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

                AuthenticationResponse authentication = new AuthenticationResponse();
                authentication.token = jwt_token;

                return Ok(authentication);
            }

            return BadRequest("Invalid email id");
        }
    }
}
