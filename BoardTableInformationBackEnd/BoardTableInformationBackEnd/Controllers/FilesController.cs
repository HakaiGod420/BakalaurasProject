using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;

namespace BoardTableInformationBackEnd.Controllers
{
    [ApiController]
    [Route("/api/upload")]
    public class FilesController : ControllerBase
    {
        [HttpPost("imagePost")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadImage([FromForm] CreateImageDto images)
        {
            var location = Directory.GetCurrentDirectory() + "\\Files\\Images\\" + images.TabletopTitle;
            try
            {  
                bool exists = System.IO.Directory.Exists(location);

                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(location);
                    var count = 0;
                    foreach(var image in images.Images)
                    {
                        var locationWithName = location + "/" + images.FileNames[count];
                        using (Stream stream = new FileStream(locationWithName, FileMode.Create))
                        {
                            image.CopyTo(stream);
                        }
                        count++;
                    }
                }
                else
                {
                    var count = 0;
                    foreach (var image in images.Images)
                    {
                        var locationWithName = location + "/" + images.FileNames[count];
                        using (Stream stream = new FileStream(locationWithName, FileMode.Create))
                        {
                            image.CopyTo(stream);
                        }
                        count++;
                    }

                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return new OkObjectResult(true);
        }

        [HttpPost("filePost")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadFile([FromForm] CreateFileDto files)
        {
            var location = Directory.GetCurrentDirectory() + "\\Files\\AditionalFiles\\" + files.TabletopTitle;
            try
            {
                bool exists = System.IO.Directory.Exists(location);

                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(location);
                    var count = 0;
                    foreach (var file in files.Files)
                    {
                        var locationWithName = location + "/" + files.FileNames[count];
                        using (Stream stream = new FileStream(locationWithName, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        count++;
                    }
                }
                else
                {
                    var count = 0;
                    foreach (var file in files.Files)
                    {
                        var locationWithName = location + "/" + files.FileNames[count];
                        using (Stream stream = new FileStream(locationWithName, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        count++;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }

            return new OkObjectResult(true);
        }
    }
}
