using System;
using Microsoft.EntityFrameworkCore;
using NSWalks.API.Data;
using NSWalks.API.Models.Domain;

namespace NSWalks.API.Repositories
{
	public class WalksRepository:IWalksRepository
	{
        private readonly NZWalksDbContext dbContext;

		public WalksRepository(NZWalksDbContext dbContext)
		{
            this.dbContext = dbContext;
		}

        public async Task<Walks> CreateAsync(Walks walk)
        {
            //get difficulty object for id
            var difficulty = await dbContext.Difficulties.FirstOrDefaultAsync(a => a.Code.Equals(walk.DifficultyCode));

            //get region object for id
            var region = await dbContext.Regions.FirstOrDefaultAsync(a => a.Code.Equals(walk.RegionCode));
            if(difficulty!=null && region != null)
            {
                walk.DifficultyId = difficulty.Id;
                walk.RegionId = region.Id;
            }

            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walks?> DeleteAsync(string code)
        {
            //find the walk
            var walk = await dbContext.Walks.Include(x=>x.Difficulty).Include(x=>x.Region).FirstOrDefaultAsync(a => a.Code.Equals(code));

            //if found delete
            if(walk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public  async Task<List<Walks>> GetAllAsync(string? filterOn = null, string? filterBy = null,string? sortBy = null, bool? isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            
            //get walks as queryable
            var walks = dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).AsQueryable();

            #region Filtering
            //apply filter on queryable
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterBy) == false)
            {
                //current business rule to allow filter on Name and Description
                if (filterOn.ToUpper().Equals(nameof(Walks.Name).ToUpper()))
                {
                    walks = walks.Where(walk => walk.Name.Contains(filterBy));
                }
                else if (filterOn.ToUpper().Equals(nameof(Walks.Description).ToUpper()))
                {
                    walks = walks.Where(walk => walk.Description.Contains(filterBy));
                }
            }
            #endregion

            #region Sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false && isAscending != null)
            {
                //current business rule to allow Sorting on Name && length
                if (sortBy.ToUpper().Equals(nameof(Walks.Name).ToUpper()))
                {
                    walks = (bool)isAscending ? walks.OrderBy(walk => walk.Name) : walks.OrderByDescending(walk => walk.Name);
                }else if (sortBy.ToUpper().Equals(nameof(Walks.LengthKms).ToUpper()))
                {
                    walks = (bool)isAscending ? walks.OrderBy(walk => walk.LengthKms) : walks.OrderByDescending(walk => walk.LengthKms);
                }
            }
            #endregion
            #region Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            #endregion
            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walks?> GetByWalkNumberAsync(string code)
        {
            var walk = await dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).FirstOrDefaultAsync(a => a.Code.Equals(code));
            return walk;
        }

        public async Task<Walks?> UpdateAsync(string code, Walks walk)
        {
            //find the object in db
            var walkDomaniModel = await dbContext.Walks.FirstOrDefaultAsync(a => a.Code.Equals(code));
            //get difficulty object for id
            var difficulty = await dbContext.Difficulties.FirstOrDefaultAsync(a => a.Code.Equals(walk.DifficultyCode));

            //get region object for id
            var region = await dbContext.Regions.FirstOrDefaultAsync(a => a.Code.Equals(walk.RegionCode));
            //if not found
            if(walkDomaniModel == null || difficulty == null || region == null)
            {
                return null;
            }
            walkDomaniModel.Code = walk.Code;
            walkDomaniModel.Name = walk.Name;
            walkDomaniModel.LengthKms = walk.LengthKms;
            walkDomaniModel.RegionCode = walk.RegionCode;
            walkDomaniModel.Description = walk.Description;
            walkDomaniModel.DifficultyCode = walk.DifficultyCode;
            walkDomaniModel.WalkImage = walk.WalkImage;
            walkDomaniModel.DifficultyCode = difficulty.Code;
            walkDomaniModel.RegionCode = region.Code;
            await dbContext.SaveChangesAsync();
            return walkDomaniModel;
        }
    }
}

