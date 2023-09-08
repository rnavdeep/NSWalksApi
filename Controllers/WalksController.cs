using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSWalks.API.CustomActionFilters;
using NSWalks.API.Models.Domain;
using NSWalks.API.Models.DTO;
using NSWalks.API.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NSWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : Controller
    {
        private readonly IMapper mapper;
        private readonly IWalksRepository walksRepository;

        public WalksController(IMapper mapper, IWalksRepository walksRepository)
        {
            this.mapper = mapper;
            this.walksRepository = walksRepository;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            //call the repository class which returns list of domain models
            var walksDomainModel = await walksRepository.GetAllAsync();

            //conver the list of domain models to dtos
            var walksDto = mapper.Map<List<WalkDto>>(walksDomainModel);

            return Ok(walksDto);
        }

        // GET api/values/5
        [HttpGet("{code}")]
        public async Task<IActionResult> Get(string code)
        {
            //call the repository to get domain model of the walk if found
            var walkDomainModel = await walksRepository.GetByWalkNumberAsync(code);

            //if found
            if (walkDomainModel != null)
            {
                var walkDto = mapper.Map<WalkDto>(walkDomainModel);
                return Ok(walkDto);
            }

            return NotFound();
        }

        // POST api/values
        [HttpPost]
        [ValidateModelAtrribute]
        public async Task<IActionResult> CreateWalk([FromBody]AddWalkRequestDto addWalkRequestDto)
        {
            //convert dto to domain model using mapper
            var addWalkRequestDominaModel = mapper.Map<Walks>(addWalkRequestDto);

            //call repository
            addWalkRequestDominaModel = await walksRepository.CreateAsync(addWalkRequestDominaModel);

            //convert domain model to dto
            var walkCreatedDto = mapper.Map<WalkDto>(addWalkRequestDominaModel);

            return Ok(walkCreatedDto);
        }

        // PUT api/values/5
        [HttpPut("{code}")]
        [ValidateModelAtrribute]
        public async Task<IActionResult> UpdateWalk(string code, [FromBody]UpdateWalkRequestDto updateWalkRequestDto)
        {
            //convert dto to domain model
            var updateWalkDomainModel = mapper.Map<Walks>(updateWalkRequestDto);
            updateWalkDomainModel = await walksRepository.UpdateAsync(code, updateWalkDomainModel);
            //if updated
            if (updateWalkDomainModel != null)
            {
                //convert back to dto
                var walkDto = mapper.Map<WalkDto>(updateWalkDomainModel);
                //return whole walk body
                return Ok(walkDto);
            }
            return NotFound();
            
         }

        // DELETE api/values/5
        [HttpDelete("{code}")]
        public async Task<IActionResult> RemoveWalk(string code)
        {
            //call repository
            var walkDomainModel = await walksRepository.DeleteAsync(code);

            //if deleted
            if (walkDomainModel != null)
            {
                //convert to dto
                var walkDto = mapper.Map<WalkDto>(walkDomainModel);
                return Ok(walkDto);
            }

            return NotFound();
        }
    }
}

