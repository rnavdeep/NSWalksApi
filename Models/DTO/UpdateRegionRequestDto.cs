using System;
using System.ComponentModel.DataAnnotations;

namespace NSWalks.API.Models.DTO
{
	public class UpdateRegionRequestDto
	{
		public UpdateRegionRequestDto()
		{
		}
        [Required]
        [MaxLength(35, ErrorMessage = "Maximum length of Name is 35 characters.")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}

