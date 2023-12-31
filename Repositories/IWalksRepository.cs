﻿using System;
using NSWalks.API.Models.Domain;

namespace NSWalks.API.Repositories
{
	public interface IWalksRepository
	{
        Task<List<Walks>> GetAllAsync(string? filterOn, string? filterBy,string? sortBy, bool? isAscending=true, int pageNumber = 1, int pageSize=100);

        Task<Walks?> GetByWalkNumberAsync(string code);

        Task<Walks> CreateAsync(Walks walk);

        Task<Walks?> UpdateAsync(string code, Walks walk);

        Task<Walks?> DeleteAsync(string code);
    }
}

