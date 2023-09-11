using System;
using Microsoft.AspNetCore.Identity;

namespace NSWalks.API.Repositories
{
	public interface ITokenRepository
	{
		string CreateJwtToken(IdentityUser user, string[] roles);
	}
}

