using ChatServer.Models.UserAccount;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models
{
	public class AppDbContext:IdentityDbContext
	{

		public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
		{

		}



		public DbSet<Groups> Groups { get; set; }
		public DbSet<GroupRoom> Rooms { get; set; }
		public void Migration()
		{
			Database.Migrate();
		}
	}
}
