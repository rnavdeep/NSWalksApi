using System;
using NSWalks.API.Models.Domain;

namespace NSWalks.API.Repositories
{
	public interface IWalkImagesRepository
	{
        Task<WalkImage?> Upload(WalkImage walkImage);
    }

}

