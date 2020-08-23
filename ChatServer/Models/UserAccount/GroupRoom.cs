using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models.UserAccount
{
	public class GroupRoom
	{

		[Key]
		public int Id { get; set; }
		public string Room { get; set; }
	}
}
