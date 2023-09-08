using System;
using System.ComponentModel.DataAnnotations;
using NSWalks.API.Models.Domain;

namespace NSWalks.API.Models.DTO
{
	public class AddWalkRequestDto
	{
		public AddWalkRequestDto()
		{
		}
        [Required]
        [MinLength(3, ErrorMessage = "Minimum code length is 3 characters.")]
        [MaxLength(5, ErrorMessage = "Maximum code length is 5 characters.")]
        public string Code { get; set; }

        [Required]
        [MaxLength(35, ErrorMessage = "Maximum length of Name is 35 characters.")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double LengthKms { get; set; }

        public string? WalkImage { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Minimum code length is 3 characters.")]
        [MaxLength(5, ErrorMessage = "Maximum code length is 5 characters.")]
        public string RegionCode { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Minimum code length is 3 characters.")]
        [MaxLength(5, ErrorMessage = "Maximum code length is 5 characters.")]
        public string DifficultyCode { get; set; }


    }
}

