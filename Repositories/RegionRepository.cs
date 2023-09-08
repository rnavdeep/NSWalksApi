using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSWalks.API.Data;
using NSWalks.API.Models.Domain;

namespace NSWalks.API.Repositories
{
	public class RegionRepository:IRegionRepository
	{
        private readonly NZWalksDbContext dbContext;
		public RegionRepository(NZWalksDbContext dbContext)
		{
            this.dbContext = dbContext;
		}

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByCodeAsync(string code)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(region => region.Code.Equals(code));
        }

        public async Task<Region> CreateAsync(Region region)
        {
            //use domain model to create Region in database
            await dbContext.Regions.AddAsync(region);

            await dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> UpdateAsync(string code, Region region)
        {
            //check for the region exist
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(region => region.Code.Equals(code));

            //item does not exist 
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region?> DeleteAsync(string code)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(region => region.Code.Equals(code));
            //find the domain model object
            if (region == null)
            {
                return null;
            }
            //delete from the db
            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();
            return region;
        }
    }
}

