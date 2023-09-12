using System;
using Microsoft.EntityFrameworkCore;
using NSWalks.API.Data;
using NSWalks.API.Models.Domain;

namespace NSWalks.API.Repositories
{
	public class WalkImagesRepository:IWalkImagesRepository
	{
        private readonly NZWalksDbContext dbContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public WalkImagesRepository(IWebHostEnvironment webHostEnvironment, NZWalksDbContext dbContext, IHttpContextAccessor httpContextAccessor)
		{
            this.dbContext = dbContext;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
		}

        public async Task<WalkImage?> Upload(WalkImage walkImage)
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

        public async Task<WalkImage?> Delete(string name)
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
    }
}

