using System;
using System.ComponentModel.DataAnnotations;

namespace NSWalks.API.Models.DTO
{
	public interface IImageDto
	{
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }

        public string? FileDescription { get; set; }
    }
    public class WalkImageDto : IImageDto
    {
        private IFormFile _file;
        private string _fileName;
        private string? _fileDescription;
        private string _walkCode;
        [Required]
        public IFormFile File { get => _file; set => _file = value; }
        [Required]
        public string FileName { get => _fileName; set => _fileName = value; }
        public string? FileDescription { get => _fileDescription; set => _fileDescription = value; }
        [Required]
        public string WalkCode { get => _walkCode; set => _walkCode = value; }
    }
    public class RegionImageDto : IImageDto
    {
        private IFormFile _file;
        private string _fileName;
        private string? _fileDescription;
        private string _regionCode;
        [Required]
        public IFormFile File { get => _file; set => _file = value; }
        [Required]
        public string FileName { get => _fileName; set => _fileName = value; }
        public string? FileDescription { get => _fileDescription; set => _fileDescription = value; }
        [Required]
        public string RegionCode { get => _regionCode; set => _regionCode = value; }
    }
}

