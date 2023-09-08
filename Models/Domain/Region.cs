using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace NSWalks.API.Models.Domain
{
	[Index(nameof(Code),IsUnique =true)]
	public class Region
	{
		public Region()
		{
			Name = "default";
			Code = "default";
		}
		public Guid Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string? RegionImageUrl { get; set; }

		//Navigation properties
		public ICollection<Walks> Walks { get; } = new List<Walks>();
	}
}

