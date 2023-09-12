using System;
using System.ComponentModel.DataAnnotations;
using NSWalks.API.Models.Domain;

namespace NSWalks.API.Models.DTO
{
	public class UploadWalkImagesRequestDto
	{
		public UploadWalkImagesRequestDto()
		{
		}
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        
        public string? FileDescription { get; set; }

        [Required]
        public string WalkCode { get; set; }
    }
}

