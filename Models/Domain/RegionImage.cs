using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSWalks.API.Models.Domain
{
	public class RegionImage
	{
		public RegionImage()
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
        public string RegionCode { get; set; }
        public Guid RegionId { get; set; }
        public Region Region { get; set; }
    }
}

