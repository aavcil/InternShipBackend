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
    public class TeachersAuthController : ControllerBase
    {
        private IAuthRepository _authRepository;
        private IAppRepository _appRepository;

        private IConfiguration _configuration;
        public TeachersAuthController(IAuthRepository authRepository, IAppRepository appRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _appRepository = appRepository;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]TeacherForRegister teacherForRegister)
        {
            if (await _authRepository.TeacherExists(teacherForRegister.UserName))
            {
                ModelState.AddModelError("UserName", "Username already exists");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacherToCreate = new TeachersLogin
            {
                Username = teacherForRegister.UserName,
            };
            var teacherCreate = new Teacher()
            {
                name = teacherForRegister.name,
                surname = teacherForRegister.surname,
                title = teacherForRegister.title,
               userGroup = teacherForRegister.userGroup
            };
            var getTeacher = await _authRepository.TeacherCreate(teacherCreate);
            var createdUser = await _authRepository.TeacherRegister(teacherToCreate, teacherForRegister.Password, getTeacher.id);
            return StatusCode(201);
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserForLogin userForLoginDto)
        {
            var user = await _authRepository.TeacherLogin(userForLoginDto.UserName, userForLoginDto.Password);

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
            var Teacher = _appRepository.getTeacher(user.teacherId);
            var teacherReturn = new TeacherLoginResponse()
            {
                tokenString = tokenString,
                Username = user.Username,
                Teacher = Teacher,
                Id = user.id

            };

            return Ok(teacherReturn);
        }
    }
}