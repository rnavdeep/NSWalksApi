using System;
using NSWalks.API.Models.Domain;

namespace NSWalks.API.Repositories
{
	public interface IDifficultyRepository
    {
        Task<List<Difficulty>> GetAllAsync(string? filterOn, string? filterBy, string? sortBy, bool? isAscending = true, int pageNumber = 1, int pageSize = 100);

        Task<Difficulty?> GetByCodeAsync(string code);

        Task<Difficulty?> CreateAsync(Difficulty region);

        Task<Difficulty?> UpdateAsync(string code, Difficulty region);

        Task<Difficulty?> DeleteAsync(string code);

    }
}

