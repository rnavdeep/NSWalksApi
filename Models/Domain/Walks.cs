using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NSWalks.API.Models.Domain
{
    [Index(nameof(Code), IsUnique = true)]
    public class Walks
	{
		public Walks()
		{
			Name = "default";
			Description = "default";
			LengthKms = 0;
		}
		public Guid Id { get; set; }
		public string Code { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int WalkNumber { get; set; }

		public string Name { get;set; }
		public string Description { get; set; }
		public double LengthKms { get; set; }
		public string? WalkImage { get; set; }

		public string RegionCode { get; set; }
		public string DifficultyCode { get; set; }
        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }

		//multiple images
		public ICollection<WalkImage>? Images { get; set; }

        //Navigation properties: define relationships between two domain classes using navigation properties
        public Difficulty Difficulty { get; set; }
		public Region Region { get; set; }
	}
}

