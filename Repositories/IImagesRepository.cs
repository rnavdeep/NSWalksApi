using System;
using NSWalks.API.Models.Domain;

namespace NSWalks.API.Repositories
{
	public interface IImagesRepository
	{
        #region Walk Images
        Task<WalkImage?> WalkImageUpload(WalkImage walkImage);
        Task<WalkImage?> WalkImageDelete(string name);
        #endregion

        #region Region Images
        Task<RegionImage?> RegionImageUpload(RegionImage regionImage);
        Task<RegionImage?> RegionImageDelete(string name);
        #endregion
    }

}

