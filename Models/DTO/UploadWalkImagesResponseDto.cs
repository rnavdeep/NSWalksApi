using System;
using NSWalks.API.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace NSWalks.API.Models.DTO
{
	public class UploadWalkImagesResponseDto
	{
		public UploadWalkImagesResponseDto()
		{
		}
        public Guid Id { get; set; }
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
        public string WalkCode { get; set; }
        public Guid WalkId { get; set; }
    }
}

