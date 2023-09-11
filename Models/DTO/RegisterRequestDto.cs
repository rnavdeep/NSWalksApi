using System;
using System.ComponentModel.DataAnnotations;

namespace NSWalks.API.Models.DTO
{
	public class RegisterRequestDto
	{
		public RegisterRequestDto()
		{
		}

        [Required]
		[DataType(DataType.EmailAddress)]
        public string Username { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		public string[] Roles { get; set; }
    }
}

