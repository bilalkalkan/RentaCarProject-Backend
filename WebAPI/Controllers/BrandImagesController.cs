using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandImagesController : ControllerBase
    {
        private IBrandImagesService _brandImagesService;

        public BrandImagesController(IBrandImagesService brandImagesService)
        {
            _brandImagesService = brandImagesService;
        }

        [HttpGet("getall")]
        public IActionResult GettAll()
        {
            var result = _brandImagesService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] IFormFile file, [FromForm] BrandImages brandImages)
        {
            var result = _brandImagesService.Add(file, brandImages);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);

        }
    }
}
