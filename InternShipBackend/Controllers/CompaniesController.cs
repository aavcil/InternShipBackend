using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InternShipBackend.Data;
using InternShipBackend.Entities;
using InternShipBackend.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace InternShipBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private IAppRepository _appRepository;

        private IConfiguration _configuration;
        public CompaniesController( IAppRepository appRepository, IConfiguration configuration)
        {
            _appRepository = appRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetCompany(int id)
        {
            var company = _appRepository.getCompany(id);
            return Ok(company);
        }
        [HttpGet("All")]
        public IActionResult GetCompanies()
        {
            var company = _appRepository.getAllCompanies();
            return Ok(company);
        }

        [HttpPost]
        public ActionResult CompanyAdd([FromBody]CompanyRequest company)
        {

            string profilePictureUrl = null;

            string result = Regex.Replace(company.logoUrl, "^data:image/[a-zA-Z]+;base64,", string.Empty);
            if (company.logoUrl != null)
            {
                profilePictureUrl = $"images/{company.name}_{Guid.NewGuid().ToString()}.jpg";
                var profilePictureFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", profilePictureUrl);
                System.IO.File.WriteAllBytes(profilePictureFilePath, Convert.FromBase64String(result));
            }

            var createCompany = new Company()
            {
              personelCount = company.personelCount,
              telephone = company.telephone,
              address = company.address,
              logoUrl = profilePictureUrl,
              mail = company.mail,
              name = company.name
            };
            _appRepository.Add(createCompany);
            _appRepository.SaveAll();
            _appRepository.setCompanyId(company.userId, createCompany.id);
            _appRepository.SaveAll();
            return Ok(createCompany);

        }
        [HttpPost("AddExist")]
        public ActionResult CompanyAdds([FromBody]ExistCompanyRequest company)
        {
           _appRepository.setCompanyId(company.userId, company.id);
            _appRepository.SaveAll();
            return Ok(true);

        }
    }
}