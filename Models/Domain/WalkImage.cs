using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSWalks.API.Models.Domain
{
	public class WalkImage
	{
		public WalkImage()
		{
		}

		public Guid Id { get; set; }

		[NotMapped]
		public IFormFile File { get; set; }
		public string FileName { get; set; }
        public string? FileDescription { get; set; }
		public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
		public string WalkCode { get; set; }
		public Guid WalkId { get; set; }
		public Walks Walk { get; set; }
	}
}

