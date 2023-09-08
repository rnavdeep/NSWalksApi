using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace NSWalks.API.Models.Domain
{
    [Index(nameof(Code), IsUnique = true)]
    public class Difficulty
	{
        public Difficulty()
		{
			Name = "default";
		}
        public Guid Id { get; set; }
		public string Code { get; set; }
        public string Name { get; set; }

        //Navigation properties
        public ICollection<Walks> Walks { get; } = new List<Walks>();
    }
}

