using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSWalks.API.CustomActionFilters;
using NSWalks.API.Data;
using NSWalks.API.Models.Domain;
using NSWalks.API.Models.DTO;
using NSWalks.API.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NSWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> GetAllRegions([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            //get data from db Domain Models
            var regions = await regionRepository.GetAllAsync(filterOn,filterQuery);

            // map domain models to dtos using automapper 
            var regionsDto = mapper.Map<List<RegionDto>>(regions);

            //return dtos for the client
            return Ok(regionsDto);
        }


        // GET api/values/5
        [HttpGet("{code}")]
        public async Task<IActionResult> GetRegionByCode(string code)
        {
            // get region domain model from db
            var region = await regionRepository.GetByCodeAsync(code);

            if(region!=null)
            {
                //map domain model to dto using mapper 
                var regionDto = mapper.Map<RegionDto>(region);
                return Ok(regionDto);
            }
            return NotFound();
        }

        // POST api/values
        [HttpPost]
        [ValidateModelAtrribute]
        public async Task<IActionResult> Create([FromBody]AddRegionRequestDto addRegionRequestDto)
        {
            // convert DTO to domain model for db
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            try
            {
                //check if the code is unique or not before adding and saving changes
                var region = await regionRepository.GetByCodeAsync(addRegionRequestDto.Code);
                if (region != null)
                {
                    //throw exception
                    throw new NotImplementedException("Code is not unique");
                }
                else
                {
                    regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                }
            }
            catch (Exception e)
            {
                var result = new BadRequestObjectResult(new { message = e.Message, currentDate = DateTime.Now });
                return result;
            }
            //map from model to dto to send back
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            //send the id created.
            return CreatedAtAction(nameof(GetRegionByCode), new { code = regionDto.Code }, regionDto);

        }

        // PUT api/values/5
        [HttpPut("{code}")]
        [ValidateModelAtrribute]
        public async Task<IActionResult> UpdateRegionByCode([FromRoute] string code, [FromBody] UpdateRegionRequestDto updateRegionRequest)
        {
            //map dto to domain model if it exists
            var regionDomainModel = mapper.Map<Region>(updateRegionRequest);

            //call the repository class
            regionDomainModel = await regionRepository.UpdateAsync(code, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //convert domain model to dto for client
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }
        // DELETE api/values/5
        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            var region = await regionRepository.DeleteAsync(code);

            if(region == null)
            {
                return NotFound();
            }
            //dto of the deleted domain model of the region
            var regionDto = mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }
    }
}

