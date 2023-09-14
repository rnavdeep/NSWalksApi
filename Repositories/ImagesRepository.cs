﻿using System;
using Microsoft.EntityFrameworkCore;
using NSWalks.API.Data;
using NSWalks.API.Models.Domain;

namespace NSWalks.API.Repositories
{
	public class ImagesRepository:IImagesRepository
	{
        private readonly NZWalksDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ImagesRepository(IWebHostEnvironment webHostEnvironment, NZWalksDbContext dbContext, IHttpContextAccessor httpContextAccessor)
		{
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
		}
        #region Walk Images
        public async Task<WalkImage?> WalkImageUpload(WalkImage walkImage)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images/WalkImages", $"{walkImage.FileName}{walkImage.FileExtension}");
            var walk = await dbContext.Walks.Where(a => a.Code.Equals(walkImage.WalkCode)).FirstOrDefaultAsync();
            if (walk == null)
            {
                return null;
            }
            else
            {
                walkImage.WalkId = walk.Id;
                walkImage.Walk = walk;
            }
            //create stream and copy from walkImage file to stream
            //uploads image to local path, image will be in Images folder in NSWalks API
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await walkImage.File.CopyToAsync(stream);
            //server/images/imageName.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/WalkImages/{walkImage.FileName}{walkImage.FileExtension}";
            //get file from running applicaiton
            walkImage.FilePath = urlFilePath;

            //add image to walkImages table
            await dbContext.WalkImages.AddAsync(walkImage);
            await dbContext.SaveChangesAsync();

            return walkImage;
        }

        public async Task<WalkImage?> WalkImageDelete(string name)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images/WalkImages", $"{name}");
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/WalkImages/{name}";
            var walkImage = await dbContext.WalkImages.Where(a => (a.FileName+a.FileExtension).Equals(name)).FirstOrDefaultAsync();
            if(walkImage == null && File.Exists(localFilePath)!=false)
            {
                return null;
            }
            File.Delete(localFilePath);
            dbContext.WalkImages.Remove(walkImage);
            await dbContext.SaveChangesAsync();
            return walkImage;
        }
        #endregion

        #region Region Images
        public async Task<RegionImage?> RegionImageUpload(RegionImage regionImage)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images/RegionImages", $"{regionImage.FileName}{regionImage.FileExtension}");
            var region = await dbContext.Regions.Where(a => a.Code.Equals(regionImage.RegionCode)).FirstOrDefaultAsync();
            if (region == null)
            {
                return null;
            }
            else
            {
                regionImage.RegionId = region.Id;
                regionImage.Region = region;
            }
            //create stream and copy from regionImage file to stream
            //uploads image to local path, image will be in Images folder in NSWalks API
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await regionImage.File.CopyToAsync(stream);
            //server/images/imageName.jpg
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/RegionImages/{regionImage.FileName}{regionImage.FileExtension}";
            //get file from running applicaiton
            regionImage.FilePath = urlFilePath;

            //add image to regionImages table
            await dbContext.RegionImages.AddAsync(regionImage);
            await dbContext.SaveChangesAsync();

            return regionImage;
        }

        public async Task<RegionImage?> RegionImageDelete(string name)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images/RegionImages", $"{name}");
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/RegionImages/{name}";
            var regionImage = await dbContext.RegionImages.Where(a => (a.FileName + a.FileExtension).Equals(name)).FirstOrDefaultAsync();
            if (regionImage == null && File.Exists(localFilePath) != false)
            {
                return null;
            }
            File.Delete(localFilePath);
            dbContext.RegionImages.Remove(regionImage);
            await dbContext.SaveChangesAsync();
            return regionImage;
        }
        #endregion
    }
}

