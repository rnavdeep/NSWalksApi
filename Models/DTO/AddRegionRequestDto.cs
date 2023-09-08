using System;
using System.ComponentModel.DataAnnotations;

namespace NSWalks.API.Models.DTO
{
	public class AddRegionRequestDto
	{
		public AddRegionRequestDto()
		{
		}
		[Required]
		[MinLength(3,ErrorMessage ="Minimum code length is 3 characters.")]
		[MaxLength(5,ErrorMessage ="Maximum code length is 5 characters.")]
        public string Code { get; set; }

		[Required]
		[MaxLength(35,ErrorMessage ="Maximum length of Name is 35 characters.")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}

