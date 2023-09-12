using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSWalks.API.Models.Domain;
using NSWalks.API.Models.DTO;
using NSWalks.API.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NSWalks.API.Controllers
{
    [Route("api/[controller]")]
    public class WalkImagesController : Controller
    {
        private readonly IWalkImagesRepository imagesRepository;
        private readonly IMapper mapper;

        public WalkImagesController(IWalkImagesRepository imagesRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.imagesRepository = imagesRepository;
        }

        // POST api/WalkImages/Upload
        [HttpPost]
        [Route("Upload")]
        [Authorize(Roles = ("Writer"))]
        public async Task<IActionResult> Upload([FromForm] UploadWalkImagesRequestDto uploadWalkImagesRequestDto)
        {
            ValidateFileUpload(uploadWalkImagesRequestDto);
            if (ModelState.IsValid)
            {
                //convert dto to domain model
                var imageDomainModel = new WalkImage
                {
                    File = uploadWalkImagesRequestDto.File,
                    FileExtension = Path.GetExtension(uploadWalkImagesRequestDto.File.FileName),
                    FileDescription = uploadWalkImagesRequestDto.FileDescription,
                    FileName = uploadWalkImagesRequestDto.FileName,
                    FileSizeInBytes = uploadWalkImagesRequestDto.File.Length,
                    WalkCode = uploadWalkImagesRequestDto.WalkCode
                };

                imageDomainModel = await imagesRepository.Upload(imageDomainModel);
                if (imageDomainModel != null)
                {
                    var responseImage = mapper.Map<UploadWalkImagesResponseDto>(imageDomainModel);
                    return Ok(responseImage);

                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest(ModelState);
        }
        private void CheckExtension(string fileName)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
            //check extensions
            if (allowedExtension.Contains(Path.GetExtension(fileName)) == false)
            {
                ModelState.AddModelError("File", "Unsupported Image Type");
            }
        }
        private void ValidateFileUpload(UploadWalkImagesRequestDto uploadWalkImagesRequestDto)
        {
            CheckExtension(uploadWalkImagesRequestDto.File.FileName);
            //check filesize
            if (uploadWalkImagesRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "Unsupported File Size");
            }

        }

        // DELETE api/values/5
        [HttpDelete("{name}")]
        [Authorize(Roles =("Writer"))]
        public async Task<IActionResult> Delete([FromForm] string name)
        {
            CheckExtension(name);
            if (ModelState.IsValid)
            {
                var walkImageDomain = await imagesRepository.Delete(name);
                if (walkImageDomain != null)
                {

                    var responseImage = mapper.Map<UploadWalkImagesResponseDto>(walkImageDomain);
                    return Ok(responseImage);
                }
            }
            return BadRequest(ModelState);

        }
    }
}

