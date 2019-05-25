using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InternShipBackend.Data;
using InternShipBackend.Entities;
using InternShipBackend.Request;
using InternShipBackend.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace InternShipBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepository _authRepository;
        private IAppRepository _appRepository;

        private IConfiguration _configuration;
        public AuthController(IAuthRepository authRepository, IAppRepository appRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _appRepository = appRepository;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegister userForRegisterDto)
        {
            if (await _authRepository.UserExists(userForRegisterDto.UserName))
            {
                ModelState.AddModelError("UserName", "Username already exists");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToCreate = new User
            {
                Username = userForRegisterDto.UserName,
            };
            var studentCreate = new Student
            {
                name = userForRegisterDto.name,
                surname = userForRegisterDto.surname,
                tcNo = userForRegisterDto.tcNo
            };
            var getStudentId = await _authRepository.StudentRegister(studentCreate);
            var createdUser = await _authRepository.Register(userToCreate, userForRegisterDto.Password,getStudentId.id);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserForLogin userForLoginDto)
        {
            var user = await _authRepository.Login(userForLoginDto.UserName, userForLoginDto.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.Now.AddYears(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                    , SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            var student = _appRepository.getStudent(user.userId);
            var userReturn = new StudentLoginResponse()
            {
                tokenString = tokenString,
                Username = user.Username,
                Student = student,
                Id = user.id

            };

            return Ok(userReturn);
        }

        [HttpGet("getId")]
        public IActionResult GetId(int id)
        {
            long studentId=_authRepository.studentId(id);
            return Ok(studentId);
        }
    }
}