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
    public class ImagesController : Controller
    {
        private readonly IImagesRepository imagesRepository;
        private readonly IMapper mapper;

        public ImagesController(IImagesRepository imagesRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.imagesRepository = imagesRepository;
        }

        #region Validations
        private void CheckExtension(string fileName)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
            //check extensions
            if (allowedExtension.Contains(Path.GetExtension(fileName)) == false)
            {
                ModelState.AddModelError("File", "Unsupported Image Type");
            }
        }
        private void ValidateFileUpload(string fileName, long fileLength)
        {
            CheckExtension(fileName);
            //check filesize
            if (fileLength > 10485760)
            {
                ModelState.AddModelError("File", "Unsupported File Size");
            }

        }
        #endregion

        #region Walk Images Endpoints
        // POST 
        [HttpPost]
        [Route("WalkImageUpload")]
        //[Authorize(Roles = ("Writer"))]
        public async Task<IActionResult> WalkImageUpload([FromForm] WalkImageDto walkImageDto)
        {
            ValidateFileUpload(walkImageDto.File.FileName,walkImageDto.File.Length);
            if (ModelState.IsValid)
            {
                //convert dto to domain model
                var imageDomainModel = new WalkImage
                {
                    File = walkImageDto.File,
                    FileExtension = Path.GetExtension(walkImageDto.File.FileName),
                    FileDescription = walkImageDto.FileDescription,
                    FileName = walkImageDto.FileName,
                    FileSizeInBytes = walkImageDto.File.Length,
                    WalkCode = walkImageDto.WalkCode
                };

                imageDomainModel = await imagesRepository.WalkImageUpload(imageDomainModel);
                if (imageDomainModel != null)
                {
                    var responseImage = mapper.Map<WalkImageDto>(imageDomainModel);
                    return Ok(responseImage);

                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest(ModelState);
        }


        // DELETE Image
        [HttpDelete]
        [Route("WalkImageDelete")]
        [Authorize(Roles =("Writer"))]
        public async Task<IActionResult> WalkImageDelete([FromForm] string name)
        {
            CheckExtension(name);
            if (ModelState.IsValid)
            {
                var walkImageDomain = await imagesRepository.WalkImageDelete(name);
                if (walkImageDomain != null)
                {

                    var responseImage = mapper.Map<WalkImageDto>(walkImageDomain);
                    return Ok(responseImage);
                }
            }
            return BadRequest(ModelState);

        }
        #endregion

        #region Region Images Endpoints
        // POST 
        [HttpPost]
        [Route("RegionImageUpload")]
        //[Authorize(Roles = ("Writer"))]
        public async Task<IActionResult> RegionImageUpload([FromForm] RegionImageDto regionImageDto)
        {
            ValidateFileUpload(regionImageDto.File.FileName,regionImageDto.File.Length);
            if (ModelState.IsValid)
            {
                //convert dto to domain model
                var imageDomainModel = new RegionImage
                {
                    File = regionImageDto.File,
                    FileExtension = Path.GetExtension(regionImageDto.File.FileName),
                    FileDescription = regionImageDto.FileDescription,
                    FileName = regionImageDto.FileName,
                    FileSizeInBytes = regionImageDto.File.Length,
                    RegionCode = regionImageDto.RegionCode
                };

                imageDomainModel = await imagesRepository.RegionImageUpload(imageDomainModel);
                if (imageDomainModel != null)
                {
                    var responseImage = mapper.Map<RegionImageDto>(imageDomainModel);
                    return Ok(responseImage);

                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest(ModelState);
        }

        // DELETE Image
        [HttpDelete]
        [Route("RegionImageDelete")]
        //[Authorize(Roles = ("Writer"))]
        public async Task<IActionResult> RegionImageDelete([FromForm] string name)
        {
            CheckExtension(name);
            if (ModelState.IsValid)
            {
                var regionImage = await imagesRepository.RegionImageDelete(name);
                if (regionImage != null)
                {

                    var responseImage = mapper.Map<RegionImageDto>(regionImage);
                    return Ok(responseImage);
                }
            }
            return BadRequest(ModelState);

        }
        #endregion
    }
}

