using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtentionTypeProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtentionContentTypeProvider)
        {
            _fileExtentionTypeProvider =
                fileExtentionContentTypeProvider
                ?? throw new System.ArgumentException(nameof(fileExtentionContentTypeProvider));
        }

        [HttpGet("{fileid}")]
        public ActionResult GetFile(string fileid)
        {
            var path = "E-Commerce Project.pdf";
            if (!System.IO.File.Exists(path))
            {
                return NotFound();
            }

            if (!_fileExtentionTypeProvider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, contentType, Path.GetFileName(path));
        }
    }
}
