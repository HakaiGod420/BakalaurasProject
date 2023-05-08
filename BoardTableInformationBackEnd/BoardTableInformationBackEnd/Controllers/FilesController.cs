using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;

namespace BoardTableInformationBackEnd.Controllers
{
    [ApiController]
    [Route("/api/upload")]
    [Authorize]
    public class FilesController : ControllerBase
    {
        [HttpPost("imagePost")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadImage([FromForm] CreateImageDto images)
        {
            if (images.FileNames.Count == 0 || images.Images.Count == 0)
            {
                return BadRequest("No Images where to send it");
            }

            if (images.TabletopTitle == null || images.TabletopTitle == "")
            {
                return BadRequest("Empty tabletop or null");
            }

            var location = Directory.GetCurrentDirectory() + "\\Files\\Images\\" + images.TabletopTitle;
            try
            {
                bool exists = System.IO.Directory.Exists(location);

                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(location);
                    var count = 0;
                    foreach (var image in images.Images)
                    {
                        var locationWithName = location + "/" + images.FileNames[count];
                        using (Stream stream = new FileStream(locationWithName, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
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
                            await image.CopyToAsync(stream);
                        }
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return new OkObjectResult(true);
        }

        [HttpPost("filePost")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
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
                            await file.CopyToAsync(stream);
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
                            await file.CopyToAsync(stream);
                        }
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }

            return new OkObjectResult(true);
        }
    }
}
