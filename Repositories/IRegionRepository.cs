using System;
using NSWalks.API.Models.Domain;

namespace NSWalks.API.Repositories
{
	public interface IRegionRepository
	{
		Task<List<Region>> GetAllAsync(string? filterOn, string? filterBy, string? sortBy, bool? isAscending = true, int pageNumber = 1, int pageSize = 100);

		Task<Region?> GetByCodeAsync(string code);

		Task<Region> CreateAsync(Region region);

		Task<Region?> UpdateAsync(string code, Region region);

		Task<Region?> DeleteAsync(string code);
	}
}

