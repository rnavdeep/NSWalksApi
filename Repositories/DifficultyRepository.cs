using System;
using Microsoft.EntityFrameworkCore;
using NSWalks.API.Data;
using NSWalks.API.Mappings;
using NSWalks.API.Models.Domain;

namespace NSWalks.API.Repositories
{
	public class DifficultyRepository:IDifficultyRepository
	{
        private readonly NZWalksDbContext dbContext;
		public DifficultyRepository(NZWalksDbContext dbContext)
		{
            this.dbContext = dbContext;
		}

        public async Task<Difficulty?> CreateAsync(Difficulty difficulty)
        {
            //use domain model to create Region in database
            await dbContext.Difficulties.AddAsync(difficulty);

            await dbContext.SaveChangesAsync();

            return difficulty;
        }

        public async Task<Difficulty?> DeleteAsync(string code)
        {
            //find if it exists
            //check for the region exist
            var deleteDifficulty = await dbContext.Difficulties.FirstOrDefaultAsync(difficulty => difficulty.Code.Equals(code));

            //item does not exist 
            if (deleteDifficulty == null)
            {
                return null;
            }

            //delete all the walks associated with difficulty
            var walksToDelelte = await dbContext.Walks.Where(walk => walk.DifficultyCode.Equals(code)).ToListAsync();
            dbContext.Walks.RemoveRange(walksToDelelte);

            //delete difficulty
            dbContext.Difficulties.Remove(deleteDifficulty);
            await dbContext.SaveChangesAsync();

            //return new difficulty object
            return deleteDifficulty;
        }

        public async Task<List<Difficulty>> GetAllAsync(string? filterOn= null, string? filterBy = null, string? sortBy = null, bool? isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            //get all difficulties as queryable
            var difficulties = dbContext.Difficulties.AsQueryable();
            #region Filtering
            //apply filter on queryable
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterBy) == false)
            {
                //current business rule to allow filter on Name 
                if (filterOn.ToUpper().Equals(nameof(Difficulty.Name).ToUpper()))
                {
                    difficulties = difficulties.Where(diff => diff.Name.Contains(filterBy));
                }
            }
            #endregion
            #region Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false && isAscending!=null)
            {
                //current business rule to allow sort on Name 
                if (sortBy.ToUpper().Equals(nameof(Difficulty.Name).ToUpper()))
                {
                    difficulties = (bool)isAscending ? difficulties.OrderBy(diff => diff.Name) : difficulties.OrderByDescending(diff => diff.Name);
                }
            }
            #endregion
            #region Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            #endregion
            return await difficulties.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Difficulty?> GetByCodeAsync(string code)
        {
            return await dbContext.Difficulties.FirstOrDefaultAsync(a => a.Code.Equals(code));
        }

        public async Task<Difficulty?> UpdateAsync(string code, Difficulty difficulty)
        {
            //find if it exists
            //check for the region exist
            var existingDifficulty = await dbContext.Difficulties.FirstOrDefaultAsync(difficulty => difficulty.Code.Equals(code));

            //item does not exist 
            if (existingDifficulty == null)
            {
                return null;
            }
            //update the Name field and save changed
            existingDifficulty.Name = difficulty.Name;
            await dbContext.SaveChangesAsync();

            //return new difficulty object
            return existingDifficulty;
        }
    }
}

