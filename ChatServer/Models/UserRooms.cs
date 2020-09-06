using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models
{
	public class UserRooms
	{
		[Key]
		public int Id { get; set; }

		public string Email { get; set; }
		public string Room { get; set; }

	}
}
