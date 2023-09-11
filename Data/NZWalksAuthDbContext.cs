﻿using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NSWalks.API.Data
{
	public class NZWalksAuthDbContext:IdentityDbContext
	{
		public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options):base(options)
		{
		}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "561855b0-fbc8-4064-ab99-17e2d85bc634";
            var writerRoleId = "ae4a84a0-55bd-4b85-a1c2-e9625173d4a4";


            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}

