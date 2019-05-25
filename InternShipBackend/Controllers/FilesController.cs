using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InternShipBackend.Data;
using InternShipBackend.Request;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using File = InternShipBackend.Entities.File;

namespace InternShipBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private IAppRepository _appRepository;
        private IHostingEnvironment _hostingEnvironment;

        public FilesController(IAppRepository appRepository, IHostingEnvironment hostingEnvironment)
        {
            _appRepository = appRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        //[HttpPost]
        //public async Task<IActionResult> UploadFile([FromBody]FileRequest req)
        //{
        //    if (req == null || req.file.Length == 0)
        //        return Content("Dosya Seçmediniz.");
        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        //    var name = Guid.NewGuid() + ".pdf";
        //    using (var stream = new FileStream(Path.Combine(path, name), FileMode.Create))
        //    {
        //        await req.file.CopyToAsync(stream);
        //        var res = new File()
        //        {
        //            path = name,
        //            studentId = req.studentId
        //        };
        //        _appRepository.Add(req);
        //        _appRepository.SaveAll();
        //    }

        //    return Ok(name);

        //}
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];


                var studentId = Request.Form["studentId"].ToString();

                string folderName = "Upload";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString();
                    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload");
                    var name = Guid.NewGuid() + ".pdf";
                    using (var stream = new FileStream(Path.Combine(fullPath, name), FileMode.Create))
                    {
                        file.CopyTo(stream);
                        var fileAdd = new File()
                        {
                            studentId = int.Parse(studentId),
                            path = name
                        };
                        _appRepository.Add(fileAdd);
                        _appRepository.SaveAll();

                    }
                }
                return Ok("Upload Successful.");
            }
            catch (Exception ex)
            {
                return Ok("Upload Failed: " + ex.Message);
            }
        }
        [HttpGet]
        public IActionResult GetFile(int id)
        {
            var res = _appRepository.GetFileResponse(id);
            return Ok(res);
        }


    }
}